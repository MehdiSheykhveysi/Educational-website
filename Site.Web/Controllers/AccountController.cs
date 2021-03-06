using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.Core.DataBase.Repositories.CustomizeIdentity;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.Interfaces;
using Site.Web.Models.AccountModels;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly CustomUserManager _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        public IEmailHandler EmailHandler { get; set; }

        public AccountController(CustomUserManager userManager, SignInManager<CustomUser> signInManager, IEmailHandler emailHandler)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.EmailHandler = emailHandler;
        }

        public IActionResult Register(string ReturnUrl = "/")
        {
            ViewBag.returnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl)
        {
            returnUrl = returnUrl ?? "/";

            CustomUser user = new CustomUser
            {
                UserName = model.Username,
                Email = model.Username,
                PhoneNumber = model.PhoneNumber,
                Avatar = "index.png",
                RegisterDate = DateTime.Now,
                AccountBalance = 0,
                IsDeleted = false
            };
            IdentityResult Result = await _userManager.CreateAsync(user, model.Password);
            if (Result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, Code = code }, protocol: Request.Scheme);
                await EmailHandler.SendEmailAsync(model.Username, "Confirm your email",
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
                    ViewBag.DoActive = true;
                    return View("ConfirmEmail");
                }
                await _signInManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult SignInResult = await _signInManager.PasswordSignInAsync(user, model.PassWord, isPersistent: model.RememberMe, lockoutOnFailure: true);
                if (SignInResult.Succeeded)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "پسوورد یا ایمیلتان اشتباه است");
                }
            }
            else
            {
                ModelState.AddModelError("", "پسوورد یا ایمیلتان اشتباه است");
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

        public IActionResult CheckEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckEmail(CheckEmailViewModel model)
        {
            CustomUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ViewData["ErrorMessage"] = "متاسفانه کاربری با چنین ایمیلی وجود ندارد";
                ViewBag.Issuccess = false;
                return View("CheckEmail");
            }
            if (!user.EmailConfirmed)
            {
                ViewData["Title"] = "فعال سازی اکانت";
                ViewData["ConfirmMessage"] = "متاسفانه حساب شما فعال نمیباشد";
                ViewBag.IsSuccess = false;
                ViewBag.DoActive = true;
                return View("ConfirmEmail");
            }
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string resetLink = Url.Action("ResetPassword",
                            "Account", new { userId = user.Id, Code = token },
                             protocol: Request.Scheme);
            await EmailHandler.SendEmailAsync(user.Email, "Change Password",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(resetLink)}'>clicking here</a>.");
            ViewData["Title"] = "تغییر پسوورد";
            ViewData["ConfirmMessage"] = "لینک تغیر پسوورد به ایمیل شما فرستاده شد";
            ViewBag.IsSuccess = true;
            return View("ConfirmEmail");
        }


        public IActionResult ResetPassword(string UserId, string Token)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel
            {
                UserId = UserId,
                Token = Token
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (model.UserId == null || model.Token == null)
            {
                ViewData["Title"] = "تغییر پسوورد";
                ViewData["ConfirmMessage"] = "مشخصات وارد شده صحیح نمیباشد";
                ViewBag.IsSuccess = false;
                return View("ConfirmEmail");
            }
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                ViewData["Title"] = "تغییر پسوورد";
                ViewData["ConfirmMessage"] = "مشخصات وارد شده صحیح نمیباشد";
                ViewBag.IsSuccess = false;
                return View("ConfirmEmail");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
            {
                ViewBag.IsSuccess = true;
                ViewData["Title"] = "تغییر پسوورد";
                ViewData["ConfirmMessage"] = "شما با موفقیت پسوورد خورد را عوض کردید";
                return View("ConfirmEmail");
            }
            else
            {
                ModelState.AddModelError("1","اطلاعات ارسالی صحیح نیس");
            }
            return View(model);
        }

        public IActionResult ActiveAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActiveAccount(CheckEmailViewModel model)
        {
            CustomUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ViewData["ErrorMessage"] = "متاسفانه کاربری با چنین ایمیلی وجود ندارد";
                ViewBag.Issuccess = false;
                return View();
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var resetLink = Url.Action("ConfirmEmail",
                            "Account", new { userId = user.Id, Code = token },
                             protocol: Request.Scheme);
            await EmailHandler.SendEmailAsync(model.Email, "Change Password",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(resetLink)}'>clicking here</a>.");
            ViewBag.IsSuccess = true;
            @ViewData["EmailAddress"] = model.Email;
            ViewBag.IsSuccess = true;
            return View();
        }

    }
}