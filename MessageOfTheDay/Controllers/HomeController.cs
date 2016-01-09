using System.Web.Mvc;

namespace MessageOfTheDay.Controllers
{
    /// <summary>
    /// The Home Controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Action to load home page
        /// </summary>
        /// <returns>Home page view</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
