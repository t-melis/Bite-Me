using System.Web.Mvc;

namespace practicingbiteme2.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {

        public ActionResult About()
        {
            return View();
        }


        // η μέθοδος για τα Products
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Contact()
        {
            return View();
        }



        public ActionResult Events()
        {
            return View();
        }
    }
}