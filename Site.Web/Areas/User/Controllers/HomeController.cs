using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.Core.Domain.Entities;
using Site.Web.Areas.User.Models.HomeModels;
using Site.Web.Infrastructures;
using Site.Web.Infrastructures.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Site.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly GetUser _getUser;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IImageHandler _imageHandler;
        public HomeController(GetUser getUser, IHostingEnvironment hostingEnvironment, UserManager<CustomUser> userManager, IImageHandler imageHandler)
        {
            _getUser = getUser;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _imageHandler = imageHandler;
        }

        public async Task<IActionResult> Index()
        {
            CustomUser LoggedUser = await _getUser.GetloggedUser(User);
            UserProfileViewModel model = new UserProfileViewModel
            {
                UserName = LoggedUser.UserName,
                Email = LoggedUser.Email,
                PhoneNumber = LoggedUser.PhoneNumber,
                RegisterDate = LoggedUser.RegisterDate.ToShamsi(),
                Wallet = LoggedUser.Wallet,
                UserProfileUrl = LoggedUser.Avatar
            };

            return View(model);
        }

        public async Task<IActionResult> EditProfile()
        {
            CustomUser loggeduser = await _getUser.GetloggedUser(User);
            EditUserProfileViewModel model = new EditUserProfileViewModel
            {
                Email = loggeduser.Email,
                PhoneNumber = loggeduser.PhoneNumber,
                UserName = loggeduser.UserName
            };
            return View("EditProfile", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditUserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images", "UserProfile");
                string strFilePath = await _imageHandler.UploadImage(model.FormFile, uploads);
                CustomUser user = await _getUser.GetloggedUser(User);
                user.Avatar = strFilePath;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.PhoneNumber = model.PhoneNumber;
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Redirect(nameof(Index));
                }
                else
                {
                    foreach (IdentityError Error in result.Errors)
                    {
                        ModelState.AddModelError(Error.Code, Error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}