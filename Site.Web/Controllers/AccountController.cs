using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Site.Core.Domain.Entities;
using Site.Web.Models.AccountModels;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        public IEmailSender _emailSender { get; set; }
        public AccountController(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register(string ReturnUrl = "/")
        {
            ViewBag.returnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model,string returnUrl)
        {
            returnUrl = returnUrl ?? "/";
            if (ModelState.IsValid)
            {
                CustomUser user = new CustomUser
                {
                    UserName = model.Username,
                    Email = model.Username,
                    PhoneNumber = model.PhoneNumber,
                    Avatar = "index.png"
                };
                var Result = await _userManager.CreateAsync(user, model.Password);
                if (Result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, Code = code }, protocol: Request.Scheme);
                    await _emailSender.SendEmailAsync(model.Username, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    ViewBag.IsRegister = true;
                    ViewBag.returnUrl = returnUrl;
                    return View(model);
                }
                else
                    foreach (var item in Result.Errors)
                    {
                        ModelState.TryAddModelError(item.Code, item.Description);
                    }
            }
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string Code)
        {
            if (userId == null || Code == null)
            {
                ViewData["Title"] = "فعال سازی اکانت";
                ViewData["ConfirmMessage"] = "مشخصات وارد شده صحیح نمیباشد";
                ViewBag.IsSuccess = false;
                return View("ConfirmEmail");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewData["Title"] = "فعال سازی اکانت";
                ViewData["ConfirmMessage"] = "مشخصات وارد شده صحیح نمیباشد";
                ViewBag.IsSuccess = false;
                return View("ConfirmEmail");
            }

            var result = await _userManager.ConfirmEmailAsync(user, Code);
            if (!result.Succeeded)
            {
                return NotFound($"Error confirming email for user with ID '{userId}':");
            }
            ViewBag.IsSuccess = true;
            ViewData["Title"] = "فعال سازی اکانت";
            ViewData["ConfirmMessage"] = "شما با موفقیت اکانت خود را فعال کردید";
            ViewBag.IsSuccess = true;
            return View();
        }

        public IActionResult LogIn(string ReturnUrl = "/")
        {
            ViewBag.returnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInViewModel model, string returnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    //if (user.LockoutEnabled)
                    //{
                    //    ViewData["Title"] = "فعال سازی اکانت";
                    //    ViewData["ConfirmMessage"] = "متاسفانه حساب شما قفل شده است";
                    //    return View("ConfirmEmail");
                    //}
                    if (!user.EmailConfirmed)
                    {
                        ViewData["Title"] = "فعال سازی اکانت";
                        ViewData["ConfirmMessage"] = "متاسفانه حساب شما فعال نمیباشد";
                        ViewBag.IsSuccess = false;
                        return View("ConfirmEmail");
                    }
                    await _signInManager.SignOutAsync();
                    var SignInResult = await _signInManager.PasswordSignInAsync(user, model.PassWord, isPersistent: model.RememberMe, lockoutOnFailure: false);
                    if (SignInResult.Succeeded)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        ModelState.TryAddModelError("", "پسوورد یا ایمیلتان اشتباه است");
                    }
                }
            }
            ViewBag.returnUrl = "/";
            return View(model);
        }
        public async Task<IActionResult> LogOut(string returnUrl = "/")
        {
            returnUrl = returnUrl ?? "/";
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);

        }
    }
}