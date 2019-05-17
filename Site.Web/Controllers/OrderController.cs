using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CookieManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Core.ApplicationService.CartServise;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using Site.Web.Infrastructures.Attributes;
using Site.Web.Infrastructures.BusinessObjects;

namespace Site.Web.Controllers
{
    public class OrderController : Controller
    {
        public OrderController(ICourseRepository CourseRepository, ICookieManager CookieManager, IOrderRepository OrderRepository, CartChecker Cart)
        {
            courseRepository = CourseRepository;
            cookieManager = CookieManager;
            orderRepository = OrderRepository;
            cart = Cart;
        }

        private readonly ICourseRepository courseRepository;
        private readonly IOrderRepository orderRepository;
        private readonly ICookieManager cookieManager;
        private readonly CartChecker cart;

        [AjaxOnly]
        public async Task<PartialViewResult> Index(string UserId, CancellationToken cancellationToken)
        {
            string AnonymousUserId = cookieManager.Get<string>("LocalhostCart");
            System.Collections.Generic.List<Order> list = await orderRepository.NoTrackEntities.Where(o => o.ClientId.ToString() == UserId || o.AnonymousUserId == AnonymousUserId).ToListAsync(cancellationToken);

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

            Core.Domain.Entities.Order order = await orderRepository.NoTrackEntities.Include(o => o.OrderDetails).ThenInclude(od => od.Course).FirstOrDefaultAsync(o => o.Id.ToString() == OrderId);
            return PartialView("_ShowOrderDetailPartialView", order);
        }

        [AjaxOnly]
        public async Task<IActionResult> DeleteOrderDetail(string OrderDetailId, string OrderId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(OrderDetailId)) return BadRequest();

            await orderRepository.DeleteOrderDetailAsync(OrderId, OrderDetailId, cancellationToken);

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
    }
}