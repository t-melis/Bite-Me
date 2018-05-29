using Microsoft.AspNet.Identity;
using practicingbiteme2.Models;
using practicingbiteme2.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace practicingbiteme2.Controllers
{
    public class MyProfileController : Controller
    {
        private readonly ApplicationDbContext db;

        public MyProfileController()
        {
            db = new ApplicationDbContext();
        }

        // GET: MyProfile
        [Authorize]
        public ActionResult Index(string message, int? id)
        {
            var userID = User.Identity.GetUserId();
            var user = db.Users.Include(u => u.Addresses)
                .Include(u => u.Orders)
                .FirstOrDefault(u => u.Id == userID);

            ViewBag.Message = message;

            var viewModel = new OrderIndexData();
            viewModel.User = user;
            viewModel.Addresses = user.Addresses;
            viewModel.Orders = user.Orders.ToList();
            
            if(id != null)
            {
                ViewBag.orderId = id.Value;

                viewModel.OrderProducts = viewModel.Orders
                                                   .Where(o => o.OrderID == id.Value)
                                                   .Single()
                                                   .OrderProducts;
            }

            return View(viewModel);
        }

        //GET
        [Authorize]
        public ActionResult LoginAndSecurity(string message)
        {
            // θα κάνει edit τα στοιχεία του λογαριασμού του ο χρήστης.
            var userID = User.Identity.GetUserId();
            var user = db.Users
                .FirstOrDefault(u => u.Id == userID);
            ViewBag.StatusMessage = message;

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }


        //GET
        [HttpGet]
        public ActionResult SpecialOffers()
        {
            // πεδίο όπου ο Admin θα μπορεί να στέλνει στον User ειδικές προσφορές τόσο επι προσωπικού όσο και συνολικά σε όλους. Queries..
            return View();
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