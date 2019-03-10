using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.Core.Domain.Entities;

namespace Site.Web.Controllers
{
    public class HomeController:Controller
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