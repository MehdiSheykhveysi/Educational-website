using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.Core.Domain.Entities;
using Site.Web.Models.AccountModels;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;

        public AccountController(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register(string ReturnUrl = "/")
        {
            RegisterViewModel model = new RegisterViewModel
            {
                ReturnUrl = ReturnUrl
            };
            return View( model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            model.ReturnUrl = model.ReturnUrl ?? "/";
            if (ModelState.IsValid)
            {
                CustomUser user = new CustomUser
                {
                    UserName = model.Username,
                    Email = model.Username,
                    PhoneNumber=model.PhoneNumber,
                    Avatar="index.png"
                };
                var Result = await _userManager.CreateAsync(user, model.Password);
                if (Result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect(model.ReturnUrl);
                }
                else
                    foreach (var item in Result.Errors)
                    {
                        ModelState.TryAddModelError(item.Code, item.Description);
                    }
            }
            return View(model);
        }
        public IActionResult LogIn(string ReturnUrl = "/")
        {
            LogInViewModel model = new LogInViewModel
            {
                ReturnUrl = ReturnUrl
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var SignInResult = await _signInManager.PasswordSignInAsync(user, model.PassWord, isPersistent: model.RememberMe, lockoutOnFailure: false);
                    if (SignInResult.Succeeded)
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        ModelState.TryAddModelError("", "پسوورد یا ایمیلتان اشتباه است");
                    }
                }
            }
            return View(model);
        }
    }
}