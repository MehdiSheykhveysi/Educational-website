using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.Core.Domain.Entities;
using Site.Web.Areas.User.Models.HomeModels;
using Site.Web.Infrastructures;
using Site.Web.Infrastructures.Attributes;
using Site.Web.Infrastructures.BusinessObjects;
using Site.Web.Infrastructures.Interfaces;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IFileHandler fileHandler;

        public HomeController(IHostingEnvironment hostingEnvironment, UserManager<CustomUser> userManager, IFileHandler FileHandler)
        {
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            fileHandler = FileHandler;
        }

        public async Task<IActionResult> Index()
        {
            CustomUser LoggedUser = await _userManager.GetUserAsync(User);
            UserProfileViewModel model = new UserProfileViewModel
            {
                UserName = LoggedUser.ShowUserName,
                Email = LoggedUser.Email,
                PhoneNumber = LoggedUser.PhoneNumber,
                RegisterDate = LoggedUser.RegisterDate.ToShamsi(),
                AccountBalance = LoggedUser.AccountBalance,
                UserProfileUrl = LoggedUser.Avatar
            };

            return View(model);
        }

        public async Task<IActionResult> EditProfile()
        {
            CustomUser loggeduser = await _userManager.GetUserAsync(User);
            UserProfileViewModel model = new UserProfileViewModel
            {
                Id = loggeduser.Id.ToString(),
                Email = loggeduser.Email,
                PhoneNumber = loggeduser.PhoneNumber,
                UserName = loggeduser.ShowUserName,
                UserProfileUrl = loggeduser.Avatar,
                RegisterDate = loggeduser.RegisterDate.ToShamsi()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UserProfileViewModel model)
        {
            CustomUser user = await _userManager.GetUserAsync(User);
            user.ShowUserName = model.UserName;
            user.PhoneNumber = model.PhoneNumber;
            user.EmailConfirmed = user.Email == model.Email;
            user.Email = model.Email;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Redirect(nameof(Index));
            }
            else
            {
                ModelState.AddModelStateError(result.Errors.Select(c => c.Description));
            }
            return View(model);
        }

        [AjaxOnly]
        public IActionResult EditProfileImage()
        {
            AjaxUserEditProfileImage model = new AjaxUserEditProfileImage
            {
                Id = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value
            };
            return PartialView("EditUserProfileIamgePartialView", model);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfileImage(AjaxUserEditProfileImage model, CancellationToken cancellationToken)
        {
            AjaxResult result = new AjaxResult("Error");

            CustomUser user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images", "UserProfile");
                string OldProfileImagePath = $"{_hostingEnvironment.WebRootPath}\\images\\UserProfile\\{user.Avatar}";
                string strFilePath = await fileHandler.UploadFileAsync(model.FormFile, uploads, cancellationToken, OldProfileImagePath);
                user.Avatar = strFilePath;
                IdentityResult Result = await _userManager.UpdateAsync(user);
                if (Result.Succeeded)
                {
                    result.Status = "Success";
                    result.RedirectUrl = "/User/Home/Index";
                    result.MessageWhenSuccessed = "تصویر پروفایل با موفقیت عوض شد";
                    return new JsonResult(result);
                }
                else
                {
                    ModelState.AddModelStateError(Result.Errors.Select(c => c.Description));
                }
            }
            ModelState.AddModelError("", "کاربر یافت نشد");
            result.AddErrrs(ModelState);
            return new JsonResult(result);
        }

        [AjaxOnly]
        public IActionResult ChangePassword()
        {
            AjaxUserChangrPassword model = new AjaxUserChangrPassword
            {
                Id = User.FindFirst(ClaimTypes.NameIdentifier).Value
            };
            return PartialView("ChangePasswordPartialView", model);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(AjaxUserChangrPassword model, CancellationToken cancellationToken)
        {
            CustomUser user = await _userManager.FindByIdAsync(model.Id);
            AjaxResult result = new AjaxResult("Error");
            if (user != null)
            {
                IdentityResult changeResult = await _userManager.ChangePasswordAsync(user, model.OldPassWord, model.NewPassWords);
                if (changeResult.Succeeded)
                {
                    result.Status = "Success";
                    result.MessageWhenSuccessed = "پسوورد با موفقیت تغییر کرد";
                    result.RedirectUrl = "/User/Home/Index";
                    return new JsonResult(result);
                }
                else
                {
                    ModelState.AddModelStateError(changeResult.Errors.Select(c => c.Description));
                }
                result.AddErrrs(ModelState);
                return new JsonResult(result);
            }
            ModelState.AddModelError("", "کاربر یافت نشد");
            result.AddErrrs(ModelState);
            return new JsonResult(result);
        }

    }
}