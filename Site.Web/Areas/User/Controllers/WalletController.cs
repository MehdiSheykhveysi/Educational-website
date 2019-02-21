using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures;
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
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;

        public WalletController(UserManager<CustomUser> userManager, GetUser getUser, IWalletRepository walletRepository, IMapper mapper)
        {
            _userManager = userManager;
            _getUser = getUser;
            _walletRepository = walletRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            Guid UserID = await _getUser.GetloggedUserID<Guid>(User);

            List<Wallet> wallets = await _walletRepository.GetWalletByUserId(UserID, cancellationToken);

            WalletTransactViewModel model = new WalletTransactViewModel
            {
                Wallets = wallets
            };
            return PartialView("WalletTransactionPartialView", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Payment(WalletTransactViewModel model, CancellationToken cancellationToken)
        {

            ValidationErrorViewModel result = new ValidationErrorViewModel();
            if (ModelState.IsValid)
            {
                PayInput input = _mapper.Map<WalletTransactViewModel, PayInput>(model);
                result.Status = "Success";
                return new JsonResult(result);
            }
            result.Status = "Error";
            foreach (var modelStateVal in ViewData.ModelState.Values)
            {
                result.Errors.AddRange(modelStateVal.Errors.Select(error => error.ErrorMessage));
            }
            return new JsonResult(result);
        }
    }
}