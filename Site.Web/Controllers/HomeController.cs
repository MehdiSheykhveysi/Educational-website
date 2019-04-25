using Microsoft.AspNetCore.Mvc;

namespace Site.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}