using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Repositories;
using Site.Core.Infrastructures.DTO;
using Site.Web.Infrastructures.Attributes;
using Site.Web.Infrastructures.Compares;
using Site.Web.Models.HomeViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Controllers
{
    public class CourseController : Controller
    {
        public CourseController(ICourseGroupRepository CourseGroupRepository, ICourseRepository CourseRepository, IOrderRepository OrderRepository)
        {
            this.courseGroupRepository = CourseGroupRepository;
            this.courseRepository = CourseRepository;
            this.orderRepository = OrderRepository;
        }
        private readonly ICourseRepository courseRepository;
        private readonly ICourseGroupRepository courseGroupRepository;
        private readonly IOrderRepository orderRepository;

        public async Task<IActionResult> Index(Models.CourseViewModel.IndexViewModel model, CancellationToken cancellationToken)
        {
            model.Paging.PagedResult = await courseRepository.GetPagedCourseAsync(model.Searchkeyvalue, false, 2, model.PageNumber, cancellationToken,
                model.Paging.SearchParameter.PriceStatusType, model.Paging.SearchParameter.OrderStatusType, model.Paging.SearchParameter.StartingPrice,
                model.Paging.SearchParameter.EndOfPrice, model.Paging.SearchParameter.CourseGroups.Where(g => g.Checked == true).Select(g => g.Id), model.KeyWordTitle);

            List<CourseGroupVm> CourseGroups = await courseGroupRepository.NoTrackEntities.Select(g => new CourseGroupVm { Id = g.Id, Checked = false, Title = g.Title }).ToListAsync(cancellationToken);

            if (model.Paging.SearchParameter.CourseGroups.Any())
            {
                CourseGroups.ForEach(g =>
                {
                    if (model.Paging.SearchParameter.CourseGroups.Contains(new CourseGroupVm { Id = g.Id }, new CourseGroupCompare()))
                        g.Checked = true;
                });
            }

            model.Paging.SearchParameter.CourseGroups = CourseGroups;

            return View(model);
        }

        [AjaxOnly]
        public async Task<IActionResult> LiveSearch(Site.Web.Models.CourseViewModel.IndexViewModel model, CancellationToken cancellationToken)
        {
            PagedResult<Core.Domain.Entities.Course> result = await courseRepository.GetPagedCourseAsync(model.Searchkeyvalue, false, 2, model.PageNumber, cancellationToken,
                model.Paging.SearchParameter.PriceStatusType, model.Paging.SearchParameter.OrderStatusType, model.Paging.SearchParameter.StartingPrice,
                model.Paging.SearchParameter.EndOfPrice, model.Paging.SearchParameter.CourseGroups.Where(g => g.Checked == true).Select(g => g.Id));

            return PartialView("_PagedCourselistPartialView", result);
        }

        public async Task<IActionResult> Detail(int CourseId, CancellationToken cancellationToken)
        {
            CourseDetailDTO Course = await courseRepository.GetCourseDetailAsync(CourseId, cancellationToken);

            if (Course == null)
                return NotFound();
            else
                Course.OrderCount = await orderRepository.GetOrderedCountAsync(CourseId, cancellationToken);

            return View(Course);

        }
    }
}