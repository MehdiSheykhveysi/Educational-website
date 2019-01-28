using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Site.Core.Domain.Entities;
using Site.Web.Areas.User.HomeModels;
using Site.Web.Infrastructures;
using System.Threading.Tasks;

namespace Site.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly GetUser _getUser;
        private readonly IHostingEnvironment _hostingEnvironment;
        public HomeController(GetUser getUser, IHostingEnvironment hostingEnvironmen)
        {
            _getUser = getUser;
            _hostingEnvironment = hostingEnvironmen;
        }

        public async Task<IActionResult> Index()
        {
            CustomUser LoggedUser = await _getUser.GetloggedUser(User);
            UserIndeViewModel model = new UserIndeViewModel
            {
                UserName = LoggedUser.UserName,
                PhoneNumber = LoggedUser.PhoneNumber,
                RegisterDate = LoggedUser.RegisterDate,
                Wallet = LoggedUser.Wallet,
                UserProfile = LoggedUser.Avatar
            };

            return View(model);
        }
    }
}