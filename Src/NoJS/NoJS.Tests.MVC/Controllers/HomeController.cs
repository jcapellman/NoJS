using Microsoft.AspNetCore.Mvc;

using NoJS.Library.Filters;

namespace NoJS.Tests.MVC.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }

        [ServiceFilter(typeof(LegacyFilterAttribute))]
        public IActionResult About() {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact() {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error() {
            return View();
        }
    }
}