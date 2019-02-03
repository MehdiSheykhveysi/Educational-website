using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.Core.Domain.Entities;
using Site.Web.Areas.User.Models.HomeModels;
using Site.Web.Infrastructures;
using Site.Web.Infrastructures.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            UserProfileViewModel model = new UserProfileViewModel
            {
                Email = loggeduser.Email,
                PhoneNumber = loggeduser.PhoneNumber,
                UserName = loggeduser.UserName,
                UserProfileUrl = loggeduser.Avatar,
                RegisterDate = loggeduser.RegisterDate.ToShamsi()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                CustomUser user = await _getUser.GetloggedUser(User);
                user.UserName = model.UserName;
                user.PhoneNumber = model.PhoneNumber;
                user.EmailConfirmed = user.Email == model.Email;
                user.Email = model.Email;
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded && !user.EmailConfirmed)
                {
                    return Redirect("/Account/LogOut");
                }
                else if (!result.Succeeded)
                {
                    foreach (IdentityError Error in result.Errors)
                    {
                        ModelState.AddModelError(Error.Code, Error.Description);
                    }
                }
                else
                {
                    return Redirect(nameof(Index));
                }
            }
            return View(model);
        }

        public IActionResult EditProfileImage()
        {

            return PartialView("EditUserProfileIamgePartialView");
        }

        [HttpPost]
        public async Task<IActionResult> EditProfileImage(AjaxUserEditProfileImage model)
        {
            ValidationErrorViewModel result = new ValidationErrorViewModel();

            if (ModelState.IsValid)
            {
                CustomUser user = await _getUser.GetloggedUser(User);
                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images", "UserProfile");
                string OldProfileImagePath = $"{_hostingEnvironment.WebRootPath}\\images\\UserProfile\\{user.Avatar}";
                string strFilePath = await _imageHandler.UploadImage(model.FormFile, uploads, OldProfileImagePath);

                user.Avatar = strFilePath;
                IdentityResult Result = await _userManager.UpdateAsync(user);
                if (Result.Succeeded)
                {
                    result.Status = "Success";
                }
                else
                {
                    result.Status = "error";
                    foreach (IdentityError Error in Result.Errors)
                    {
                        ModelState.AddModelError(Error.Code, Error.Description);
                    }
                }
            }
            else
            {
                result.Status = "error";
                foreach (var modelStateVal in ViewData.ModelState.Values)
                {
                    result.Errors.AddRange(modelStateVal.Errors.Select(error => error.ErrorMessage));
                }
            }

            return new JsonResult(result);
        }

    }
}