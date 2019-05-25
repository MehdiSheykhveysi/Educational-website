using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CookieManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Site.Core.ApplicationService.CartServise;
using Site.Core.ApplicationService.SiteSettings;
using Site.Core.DataBase.Repositories;
using Site.Core.DataBase.Repositories.CustomizeIdentity;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.Utilities;
using Site.Web.Areas.User.Models.WalletModels;
using Site.Web.Infrastructures.Attributes;
using Site.Web.Infrastructures.BusinessObjects;
using Site.Web.Infrastructures.Interfaces;
using Site.Core.Infrastructures.Utilities.Extensions;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Site.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        public OrderController(ICourseRepository CourseRepository, ICookieManager CookieManager, IHostingEnvironment HostingEnvironment, IOrderRepository OrderRepository, IDisCountRepository DisCountRepository, ICourseEpisodRepository CourseEpisodRepository, IPayment Payment, IMapper Mapper, CustomUserManager CustomUserManager, IOptionsSnapshot<SiteSetting> SiteSetting, CartChecker Cart)
        {
            courseRepository = CourseRepository;
            cookieManager = CookieManager;
            orderRepository = OrderRepository;
            disCountRepository = DisCountRepository;
            courseEpisodRepository = CourseEpisodRepository;
            hostingEnvironment = HostingEnvironment;
            payment = Payment;
            mapper = Mapper;
            customUserManager = CustomUserManager;
            siteSetting = SiteSetting.Value;
            cart = Cart;
        }

        private readonly ICourseRepository courseRepository;
        private readonly IOrderRepository orderRepository;
        private readonly ICookieManager cookieManager;
        private readonly IDisCountRepository disCountRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IPayment payment;
        private readonly IMapper mapper;
        private readonly ICourseEpisodRepository courseEpisodRepository;
        private readonly CustomUserManager customUserManager;
        private readonly SiteSetting siteSetting;
        private readonly CartChecker cart;

        [AjaxOnly]
        public async Task<PartialViewResult> Index(string UserId, CancellationToken cancellationToken)
        {
            string AnonymousUserId = cookieManager.Get<string>("LocalhostCart");
            List<Order> list = await orderRepository.NoTrackEntities.Where(o => o.ClientId.ToString() == UserId || o.AnonymousUserId == AnonymousUserId).ToListAsync(cancellationToken);

            return PartialView("_UserOrderListPartialView", list);
        }

        [AjaxOnly]
        public async Task<JsonResult> Create(int CourseID, CancellationToken cancellationToken)
        {
            string AnonymousUserId = cookieManager.GetOrSet("LocalhostCart", () => DateTime.Now.ToString("yyyyMMddHHmmss"), new CookieOptions { Expires = DateTime.Now.AddMonths(6) });

            string CurrentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null;

            Guid GuidUserId = new Guid();
            if (CurrentUserId != null)
                GuidUserId = Guid.Parse(CurrentUserId);

            bool result = await cart.AddAsync(CourseID, GuidUserId, AnonymousUserId, cancellationToken, User.Identity.IsAuthenticated);

            AjaxResult ajaxResult = new AjaxResult("Error");

            if (result)
            {
                ajaxResult.Status = "Success";

                ajaxResult.MessageWhenSuccessed = "دوره با موفقیت به سبد خرید شما اضافه شد";
            }
            else
                ajaxResult.Errors.Add("شما قبلا در این دوره شرکت کرده اید");

            return new JsonResult(ajaxResult);
        }

        [AjaxOnly]
        public async Task<IActionResult> Detail(string OrderId)
        {
            if (string.IsNullOrEmpty(OrderId)) return BadRequest();

            Order order = await orderRepository.NoTrackEntities.Include(o => o.OrderDetails).ThenInclude(od => od.Course).FirstOrDefaultAsync(o => o.Id.ToString() == OrderId);
            return PartialView("_ShowOrderDetailPartialView", order);
        }

        [AjaxOnly]
        public async Task<IActionResult> DeleteOrderDetail(string OrderDetailId, string OrderId, string CourseId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(OrderDetailId) || string.IsNullOrEmpty(OrderId) || string.IsNullOrEmpty(CourseId)) return BadRequest();

            await cart.DeleteOrderDetailAsync(OrderDetailId, OrderId, CourseId, cancellationToken);

            return RedirectToAction(nameof(Detail), new { OrderId });
        }

        [AjaxOnly]
        public async Task<IActionResult> Delete(string OrderId, string ClientId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(OrderId) || string.IsNullOrEmpty(ClientId)) return BadRequest();

            Order order = new Order
            {
                Id = Guid.Parse(OrderId)
            };
            await orderRepository.DeleteAsync(order, cancellationToken);
            return RedirectToAction(nameof(Index), new { UserId = ClientId });
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<JsonResult> Pay(string DisCountTitle, string OrderId, CancellationToken cancellationToken)
        {
            AjaxResult result = new AjaxResult("Error");
            PayInput input = new PayInput();
            PaymentRequest response = new PaymentRequest();
            CustomUser user = new CustomUser();
            DisCount DisCount = new DisCount();

            if (string.IsNullOrEmpty(OrderId))
            {
                result.Errors.Add("اطلاعات ارسالی صحیح نیس");
                return new JsonResult(result);
            }

            Order order = await orderRepository.GetByIdAsync(Guid.Parse(OrderId), cancellationToken);

            if (order == null)
            {
                result.Errors.Add("اطلاعات ارسالی صحیح نیس");
                return new JsonResult(result);
            }

            input.Deposits = order.TotalPrice;

            if (!string.IsNullOrEmpty(DisCountTitle))
            {
                DisCount = await disCountRepository.GetByTitle(DisCountTitle.Trim(), cancellationToken);
                input.Deposits = DisCount != null ? DisCountHelper.ComputeDisCount(DisCount, order.TotalPrice) : order.TotalPrice;
            }

            if (User.Identity.IsAuthenticated)
            {
                user = await customUserManager.GetUserAsync(User);
            }
            else
            {
                string anonymousUserId = cookieManager.Get<string>("LocalhostCart");

                user = await orderRepository.GetAnonymousUser(anonymousUserId, cancellationToken);
            }

            input.Deposits = DisCountHelper.WithdrawFromWallet(user, input.Deposits);

            input.PhoneNumber = user?.PhoneNumber;

            if (input.Deposits >= 1000)
            {
                input.Redirect = siteSetting.BuyCourseCallBackUrl;
                input.Description = "تصویه صورت حساب با احتساب تخفیف و کسر از کیف پولتان";
                response = await payment.PayAsync(input, cancellationToken);
            }
            else
            {
                result.Status = "Success";
                result.RedirectUrl = "";
                result.MessageWhenSuccessed = " مبلغ با محاسبه تخفیف و کسر از کیف پول پرداخت شد";
                DisCount.Count = DisCount.Count > 0 ? --DisCount.Count : DisCount.Count;
                await customUserManager.UpdateAsync(user);
                order.IsBought = true;
                await disCountRepository.UpdateAsync(DisCount, cancellationToken);
                return new JsonResult(result);
            }
            if (Assert.NotNull(response) && response.Status == 1 && Assert.NotNull(response.Token))
            {
                result.Status = "Success";
                result.RedirectUrl = siteSetting.RedirectUrl + response.Token;
                user.PaymentToken = response.Token;
                DisCount.Count = DisCount.Count > 0 ? DisCount.Count-- : DisCount.Count;
                await customUserManager.UpdateAsync(user);
                order.IsBought = true;
                await disCountRepository.UpdateAsync(DisCount, cancellationToken);

                return new JsonResult(result);
            }
            else
            {
                result.Errors.Add("عملیات پرداخت با شکست مواجه شد لطفا بعدا امتحان کنید");
                result.Errors.Add(response.ErrorMessage);
            }

            return new JsonResult(result);
        }
        //https://localhost:5001/Order/Verify?status=1&token=dA
        [AllowAnonymous]
        public async Task<IActionResult> Verify(VerifyInput verifyInput, CancellationToken cancellationToken)
        {
            VerifyResponse verifyResponse = await payment.VerifyAsync(verifyInput.Token, cancellationToken);
            VerifyViewModel model = mapper.Map<VerifyViewModel>(verifyResponse);
            model.Message = "عملیات انجام نشد";
            if (verifyResponse.Status == "1" && verifyResponse.Message == "OK")
            {
                model.Message = "عملیات با موفقیت انجام شد ";
                CustomUser user;
                if (User.Identity.IsAuthenticated)
                    user = await customUserManager.GetUserAsync(User);
                else
                    user = await customUserManager.GetByToken(verifyInput.Token, cancellationToken);

                user.Transactions = new List<Transact> {
                        new Transact
                    {
                        Balance = verifyResponse.Amount.ToDecimal(),
                        Description = verifyResponse.Description,
                        TransactType =TransactType.Payment,
                        TransactDate = DateTime.Now,
                        CustomUserId = user.Id,
                        TransactId=verifyResponse.TransId
                    }
                };
                user.PaymentToken = string.Empty;
                await customUserManager.UpdateAsync(user);
            }
            return View("~/Areas/User/Views/Wallet/Verify.cshtml", model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> DownloadFile(int EpisodId, CancellationToken cancellationToken)
        {
            CourseEpisod selectedEpisod = await courseEpisodRepository.GetByIdAsync(EpisodId, cancellationToken);

            string filepath = Path.Combine(hostingEnvironment.WebRootPath, "CourseDemo", "EpisodVideo", selectedEpisod.FileName);

            if (selectedEpisod.IsFree)
            {
                byte[] file = System.IO.File.ReadAllBytes(filepath);
                return File(file, "application/force-download", selectedEpisod.FileName);
            }

            if (User.Identity.IsAuthenticated)
            {
                if (orderRepository.IsBuyByUser(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), selectedEpisod.CourseId))
                {
                    byte[] file = System.IO.File.ReadAllBytes(filepath);
                    return File(file, "application/force-download", selectedEpisod.FileName);
                }
            }

            TempData["Message"] = "ابتدا باید دوره را خریداری کنید";

            return Redirect($"/Course/Detail?CourseId={selectedEpisod.CourseId}");
        }
    }
}