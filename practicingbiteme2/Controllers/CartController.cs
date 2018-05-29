using Microsoft.AspNet.Identity;
using practicingbiteme2.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace practicingbiteme2.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext db;

        public CartController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Cart
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();

            var cart = db.Carts.Include(c => c.CartProducts)
                .FirstOrDefault(c => c.CustomerID == userID);
            decimal total = 0;
            if (cart != null)
            {
                foreach (var item in cart.CartProducts)
                {
                    total += item.Product.Price * item.Quantity;
                }
                ViewBag.Total = total;
            }

            return View(cart);
        }

        [Authorize]
        public ActionResult AddToCart(int? productId)
        {
            var userId = User.Identity.GetUserId();

            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cart = db.Carts.FirstOrDefault(c => c.CustomerID == userId);

            if (cart == null)
            {
                cart = new Cart { CustomerID = userId, Customer = db.Users.Find(userId) };
            }

            var product = db.Products.Find(productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            var cartProduct = db.CartProducts.FirstOrDefault(c => c.CustomerID == userId && c.ProductID == productId);

            if (cartProduct == null)
            {
                cartProduct = new CartProduct { Cart = cart, Product = product, Quantity = 1 };
                cart.CartProducts.Add(cartProduct);
                product.CartProducts.Add(cartProduct);
            }
            else
            {
                cartProduct.Quantity++;
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult RemoveFromCart(int? productId)
        {
            var userId = User.Identity.GetUserId();
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cart = db.Carts.FirstOrDefault(c => c.CustomerID == userId);
            var product = db.Products.Find(productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            var cartProduct = db.CartProducts.First(c => c.CustomerID == userId && c.ProductID == productId);

            cart.CartProducts.Remove(cartProduct);
            product.CartProducts.Remove(cartProduct);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateQuantity(int? productId, int? quantity)
        {
            var userId = User.Identity.GetUserId();
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (quantity == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cartProduct = db.CartProducts.FirstOrDefault(c => c.CustomerID == userId && c.ProductID == productId);
            cartProduct.Quantity = quantity ?? 1;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult SendToWishlist(int? productId)
        {
            var userId = User.Identity.GetUserId();
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cart = db.Carts.Find(userId);
            var wishlist = db.Wishlists.Find(userId);
            if (wishlist == null)
            {
                wishlist = new Wishlist { CustomerID = userId };
            }
            var product = db.Products.Find(productId);
            if (product == null)
            {
                return HttpNotFound();
            }
            var cartProduct = db.CartProducts.FirstOrDefault(c => c.CustomerID == userId && c.ProductID == productId);

            cart.CartProducts.Remove(cartProduct);
            product.CartProducts.Remove(cartProduct);
            wishlist.Products.Add(product);
            product.Wishlists.Add(wishlist);
            db.SaveChanges();
            return RedirectToAction("Index", "Wishlist");
        }

        [Authorize]
        public ActionResult EmptyCart()
        {
            var userId = User.Identity.GetUserId();
            var cart = db.Carts.FirstOrDefault(c => c.CustomerID == userId);
            cart.CartProducts.Clear();
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ConfirmOrder()
        {
            var userID = User.Identity.GetUserId();
            var cart = db.Carts.Include(c => c.CartProducts).First(u => u.CustomerID == userID);
            decimal total = 0;
            foreach (var item in cart.CartProducts)
            {
                total += item.Product.Price * item.Quantity;
            }
            ViewBag.Total = total;
            return View(cart);
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