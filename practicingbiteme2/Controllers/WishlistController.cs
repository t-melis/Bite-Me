using Microsoft.AspNet.Identity;
using practicingbiteme2.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace practicingbiteme2.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext db;

        public WishlistController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Wishlist
        [Authorize]
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();

            var wishlist = db.Wishlists.Include(w => w.Products)
                .FirstOrDefault(w => w.CustomerID == userID);

            return View(wishlist);
        }

        [Authorize]
        public ActionResult AddToWishlist(int? productId)
        {
            var userId = User.Identity.GetUserId();

            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var wishlist = db.Wishlists.Find(userId);
            if (wishlist == null)
            {
                wishlist = new Wishlist()
                {
                    CustomerID = userId
                };
            }

            var product = db.Products.Find(productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            wishlist.Products.Add(product);
            product.Wishlists.Add(wishlist);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult RemoveFromWishlist(int? productId)
        {
            var userId = User.Identity.GetUserId();

            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var wishlist = db.Wishlists.FirstOrDefault(w => w.CustomerID == userId);

            var product = db.Products.Find(productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            wishlist.Products.Remove(product);
            product.Wishlists.Remove(wishlist);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult SendToCart(int? productId)
        {
            var userId = User.Identity.GetUserId();

            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var wishlist = db.Wishlists.FirstOrDefault(w => w.CustomerID == userId);

            var product = db.Products.Find(productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            var cart = db.Carts.FirstOrDefault(c => c.CustomerID == userId);
            if (cart == null)
            {
                cart = new Cart { CustomerID = userId, Customer = db.Users.Find(userId) };
            }

            wishlist.Products.Remove(product);
            product.Wishlists.Remove(wishlist);

            var cartProduct = db.CartProducts.FirstOrDefault(c => c.CustomerID == userId && c.ProductID == productId);

            if (cartProduct == null)
            {
                cartProduct = new CartProduct { Cart = cart, Product = product, Quantity = 1 };
                cart.CartProducts.Add(cartProduct);
                product.CartProducts.Add(cartProduct);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Cart");
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