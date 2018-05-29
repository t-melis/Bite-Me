using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using PayPal.Api;
using practicingbiteme2.Models;
using Order = practicingbiteme2.Models.Order;

namespace practicingbiteme2.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext db;

        public OrderController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Order of Admin
        [Authorize(Roles = "Admin")]
        public ActionResult ViewAllOrders(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var orders = db.Orders.ToList();

            return View(orders.OrderByDescending(o => o.OrderDate).ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var cart = db.Carts.Include(c => c.CartProducts).Include(c => c.CartProducts.Select(p => p.Product))
                .FirstOrDefault(c => c.CustomerID == userId);
            if (!cart.CartProducts.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var address = db.DeliveryAddresses.First(a => a.UserID == userId);
            var order = new Order
            {
                CustomerID = userId,
                OrderDate = DateTime.Now,
                Status = Status.Pending,
                OrderProducts = new List<OrderProduct>(),
                Address = address
            };
            while (cart.CartProducts.Any())
            {
                var product = cart.CartProducts.Select(c => c.Product).First();
                var quantity = cart.CartProducts.First().Quantity;
                var orderProduct = new OrderProduct { Product = product, Order = order, Quantity = quantity };
                order.OrderProducts.Add(orderProduct);
                product.OrderProducts.Add(orderProduct);
                var cartProduct = cart.CartProducts.First();
                cart.CartProducts.Remove(cartProduct);
            }

            db.SaveChanges();

            return View(order);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult EditAllOrders(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult EditAllOrders([Bind(Include = "OrderID,Status,CustomerID,OrderDate,DeliveryAddressID")] Order order)
        {

            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewAllOrders");
            }

            return View(order);
        }


        //[Authorize]
        //[HttpGet]
        //public ActionResult EditMyOrder(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);                           
        //}

        //[HttpPost]                    //Κάνει edit ο χρήστης τα στοιχεία της παραγγελίας του εδώ με viewModel θα παίξουμε την Post
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditMyOrder([Bind(Include = "OrderID,Status,CustomerID,OrderDate,DeliveryAddressID")] Order order)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(order).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("ViewAllOrders");
        //    }

        //    return View(order);
        //}



        [Authorize]
        [HttpGet]
        public ActionResult SelectAddress(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            PopulateAddressDropDownList();

            return View(order);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("SelectAddress")]
        public ActionResult SelectAddressPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = db.Orders.Find(id);

            if (TryUpdateModel(order, "", new string[] { "DeliveryAddressID" }))
            {
                db.SaveChanges();
                return RedirectToAction("Index", "Home", new { message = "Your order is placed successfully!!!" });
            }

            PopulateAddressDropDownList(order.DeliveryAddressID);

            return View(order);
        }



        private void PopulateAddressDropDownList(object selectedAddress = null)
        {
            var userId = User.Identity.GetUserId();

            var addresses = db.DeliveryAddresses
                              .Where(a => a.UserID == userId && a.IsDeleted == false)
                              .Select(a => new
                              {
                                  text = a.ReceiverName + ", " + a.City + " " + a.Street + " " + a.Number + " " + a.PostCode,
                                  value = a.DeliveryAddressID
                              })
                              .ToList();

            ViewBag.DeliveryAddresses = new SelectList(addresses, "value", "text", selectedAddress); //ola ta properties
        }


        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Order/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            //on successful payment, show success page to user.  
            return View("SuccessView");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {
                name = "Item Name comes here",
                currency = "USD",
                price = "1",
                quantity = "1",
                sku = "sku"
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "1"
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = "3", // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = "your generated invoice number", //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }








        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}