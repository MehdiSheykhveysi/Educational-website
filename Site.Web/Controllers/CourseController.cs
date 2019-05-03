using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Repositories;
using Site.Core.Infrastructures.Utilities.Enums;
using Site.Web.Models.HomeViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Controllers
{
    public class CourseController : Controller
    {
        public CourseController(ICourseGroupRepository CourseGroupRepository, ICourseRepository CourseRepository)
        {
            this.courseGroupRepository = CourseGroupRepository;
            this.courseRepository = CourseRepository;
        }
        private readonly ICourseRepository courseRepository;
        private readonly ICourseGroupRepository courseGroupRepository;

        public async Task<IActionResult> Index(Site.Web.Models.CourseViewModel.IndexViewModel model, CancellationToken cancellationToken)
        {
            
            model.PagedResult = await courseRepository.GetPagedCourseAsync(model.Searchkeyvalue, false, 8, model.PageNumber, cancellationToken, model.SearchParameter.PriceStatusType, model.SearchParameter.OrderStatusType, model.SearchParameter.StartingPrice, model.SearchParameter.EndOfPrice, model.SearchParameter.CourseGroups.Where(g => g.Checked == true).Select(g => g.Id));

            List<CourseGroupVm> CourseGroups = await courseGroupRepository.NoTrackEntities.Select(g => new CourseGroupVm { Id = g.Id, Checked = false, Title = g.Title }).ToListAsync(cancellationToken);

            if (model.SearchParameter.CourseGroups != null)
            {
                CourseGroups.ForEach(c =>
                {
                    if (model.SearchParameter.CourseGroups.Any(g => g.Id == c.Id))
                    {
                        c.Checked = true;
                    }
                });
            }

            model.SearchParameter.CourseGroups = CourseGroups;

            return View(model);
        }
    }
}