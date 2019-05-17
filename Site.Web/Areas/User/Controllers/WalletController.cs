using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Site.Core.Infrastructures.Utilities.Extensions;
using Site.Core.ApplicationService.SiteSettings;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.Utilities;
using Site.Web.Areas.User.Models.HomeModels;
using Site.Web.Areas.User.Models.WalletModels;
using Site.Web.Infrastructures.BusinessObjects;
using Site.Web.Infrastructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Site.Web.Infrastructures.Attributes;
using Site.Core.DataBase.Repositories.CustomizeIdentity;
using System.Security.Claims;

namespace Site.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class WalletController : Controller
    {
        private readonly CustomUserManager UserManager;
        private readonly ITransactRepository TransactRepository;
        private readonly IMapper Mapper;
        private readonly IPayment Payment;
        private readonly SiteSetting SiteSetting;

        public WalletController(CustomUserManager userManager, ITransactRepository transactRepository, IMapper mapper, IPayment payment, IOptionsSnapshot<SiteSetting> siteSetting)
        {
            this.UserManager = userManager;
            this.TransactRepository = transactRepository;
            this.Mapper = mapper;
            this.Payment = payment;
            this.SiteSetting = siteSetting.Value;
        }

        public int VerifyVieModel { get; private set; }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            Guid UserID = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            List<Transact> wallets = await TransactRepository.GetWalletByUserIdAsync(UserID, cancellationToken);

            WalletTransactViewModel model = new WalletTransactViewModel
            {
                Wallets = wallets
            };
            return PartialView("WalletTransactionPartialView", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public async Task<IActionResult> SendPayment(WalletTransactViewModel model, CancellationToken cancellationToken)
        {

            AjaxResult result = new AjaxResult("Error");
            if (ModelState.IsValid)
            {
                PayInput input = new PayInput();
                CustomUser user = await UserManager.GetUserAsync(User);
                input.Deposits = model.Deposits;
                input.Description = "شارژ حساب";
                input.Redirect = SiteSetting.CallBackUrl;
                input.PhoneNumber = user.PhoneNumber;
                PaymentRequest response = await Payment.PayAsync(input, cancellationToken); ;
                if (Assert.NotNull(response) && response.Status == 1 && Assert.NotNull(response.Token))
                {
                    result.Status = "Success";
                    result.RedirectUrl = SiteSetting.RedirectUrl + response.Token;
                    return new JsonResult(result);
                }
                else
                {
                    result.Errors.Add(response.ErrorMessage);
                }

                return new JsonResult(result);
            }
            result.AddErrors(ModelState);
            return new JsonResult(result);
        }


        [AllowAnonymous]
        public async Task<IActionResult> Verify(VerifyInput verifyInput, CancellationToken cancellationToken)
        {
            VerifyResponse verifyResponse = await Payment.VerifyAsync(verifyInput.Token, cancellationToken);
            VerifyViewModel model = Mapper.Map<VerifyViewModel>(verifyResponse);
            model.Message = "عملیات شارژ انجام نشد";
            if (verifyResponse.Status == "1" && verifyResponse.Message == "OK")
            {
                model.Message = "عملیات شارژ با موفقیت انجام شد";
                CustomUser user = await UserManager.GetUserAsync(User);
                user.Transactions = new List<Transact> {
                        new Transact
                    {
                        Balance = verifyResponse.Amount.ToDecimal(),
                        Description = verifyResponse.Description,
                        TransactType =TransactType.Creditor,
                        TransactDate = DateTime.Now,
                        CustomUserId = user.Id,
                        TransactId=verifyResponse.TransId
                    }
                };
                user.AccountBalance += verifyResponse.Amount.ToDecimal();
                await UserManager.UpdateAsync(user);
            }
            return View(model);
        }
    }
}