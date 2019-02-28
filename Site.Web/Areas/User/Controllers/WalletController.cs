using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Site.Core.Infrastructures.Utilities.Extensions;
using Site.Core.ApplicationService.SiteSettings;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.Utilities;
using Site.Web.Areas.User.Models.HomeModels;
using Site.Web.Areas.User.Models.WalletModels;
using Site.Web.Infrastructures;
using Site.Web.Infrastructures.BusinessObjects;
using Site.Web.Infrastructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class WalletController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly GetUser _getUser;
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;
        private readonly IPayment payment;
        private readonly SiteSetting siteSetting;

        public WalletController(UserManager<CustomUser> userManager, GetUser getUser, IWalletRepository walletRepository, IMapper mapper, IPayment Payment, IOptionsSnapshot<SiteSetting> SiteSetting)
        {
            _userManager = userManager;
            _getUser = getUser;
            _walletRepository = walletRepository;
            _mapper = mapper;
            payment = Payment;
            siteSetting = SiteSetting.Value;
        }

        public int VerifyVieModel { get; private set; }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            Guid UserID = await _getUser.GetloggedUserID<Guid>(User);

            List<Transact> wallets = await _walletRepository.GetWalletByUserId(UserID, cancellationToken);

            WalletTransactViewModel model = new WalletTransactViewModel
            {
                Wallets = wallets
            };
            return PartialView("WalletTransactionPartialView", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(WalletTransactViewModel model, CancellationToken cancellationToken)
        {

            ValidationErrorViewModel result = new ValidationErrorViewModel();
            if (ModelState.IsValid)
            {
                PayInput input = new PayInput();
                CustomUser user = await _getUser.GetloggedUser(User);
                input.Deposits = model.Deposits;
                input.Description = "شارژ حساب";
                input.Redirect = "https://localhost:5001/User/Wallet/Verify";
                input.PhoneNumber = user.PhoneNumber;
                PaymentRequest response = await payment.PayAsync(input, cancellationToken); ;
                if (Assert.NotNull(response) && response.Status == 1 && Assert.NotNull(response.Token))
                {
                    result.Status = "Success";
                    result.RedirectUrl = siteSetting.RedirectUrl + response.Token;
                    return new JsonResult(result);
                }
                else
                {
                    result.Status = "Error";
                    result.Errors.Add(response.ErrorMessage);
                }

                return new JsonResult(result);
            }
            result.Status = "Error";
            foreach (var modelStateVal in ViewData.ModelState.Values)
            {
                result.Errors.AddRange(modelStateVal.Errors.Select(error => error.ErrorMessage));
            }
            return new JsonResult(result);
        }


        [AllowAnonymous]
        public async Task<IActionResult> Verify(VerifyInput verifyInput, CancellationToken cancellationToken)
        {
            VerifyResponse verifyResponse = await payment.VerifyAsync(verifyInput.Token, cancellationToken);
            VerifyViewModel model = _mapper.Map<VerifyViewModel>(verifyResponse);
            model.Message = "عملیات شارژ انجام نشد";
            if (verifyResponse.Status == "1" && verifyResponse.Message == "OK")
            {
                model.Message = "عملیات شارژ با موفقیت انجام شد";
                CustomUser user = await _getUser.GetloggedUser(User);
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
                await _userManager.UpdateAsync(user);
            }
            return View(model);
        }
    }
}