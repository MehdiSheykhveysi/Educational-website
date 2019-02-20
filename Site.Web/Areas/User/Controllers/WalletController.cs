using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using Site.Web.Areas.User.Models.HomeModels;
using Site.Web.Areas.User.Models.WalletModels;
using Site.Web.Infrastructures;
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
        private readonly IWalletRepository walletRepository;

        public WalletController(UserManager<CustomUser> userManager, GetUser getUser, IWalletRepository WalletRepository)
        {
            _userManager = userManager;
            _getUser = getUser;
            walletRepository = WalletRepository;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            Guid UserID =await _getUser.GetloggedUserID<Guid>(User);

            List<Wallet> wallets =await walletRepository.GetWalletByUserId(UserID, cancellationToken);

            WalletTransactViewModel model = new WalletTransactViewModel
            {
                Wallets=wallets
            };
            return PartialView("WalletTransactionPartialView", model);
        }

        public IActionResult Payment(WalletTransactViewModel model,CancellationToken cancellationToken)
        {
            ValidationErrorViewModel result = new ValidationErrorViewModel();
            if (ModelState.IsValid)
            {

            }
            return new JsonResult(result);
        }
    }
}