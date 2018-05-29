using Microsoft.AspNet.Identity;
using practicingbiteme2.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace practicingbiteme2.Controllers
{
    public class DeliveryAddressController : Controller
    {
        private readonly ApplicationDbContext db;

        public DeliveryAddressController()
        {
            db = new ApplicationDbContext();
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddAddress()
        {            
            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAddress([Bind(Include = "ReceiverName,City,Street,Number,PostCode")] DeliveryAddress address)
        {
            var userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                address.UserID = userId;
                db.DeliveryAddresses.Add(address);
                db.SaveChanges();
                return RedirectToAction("Index", "MyProfile");
            }

            return View(address);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var userId = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var address = db.DeliveryAddresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeliveryAddressID,UserID,ReceiverName,City,Street,Number,PostCode")] DeliveryAddress address)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                address.UserID = userId;
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "MyProfile", new { message = "Your changes have been saved." });
            }

            return View(address);
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteAddress(int? id)
        {
            var userId = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var address = db.DeliveryAddresses.Find(id);    // βρες την διεύθυνση με βάση το id
            
            var LIST = db.DeliveryAddresses.Where(u => u.UserID == userId);
            var firstAddress = LIST.First();
            //ViewBag.userIdList = LIST.GroupBy(a => a.DeliveryAddressID).Select(a => a.Min(d => d.DeliveryAddressID));
            if (id == firstAddress.DeliveryAddressID)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAddress(int id)
        {
            var userId = User.Identity.GetUserId();
            var address = db.DeliveryAddresses.Find(id);
            address.IsDeleted = true;
            db.SaveChanges();

            return RedirectToAction("Index", "MyProfile");
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