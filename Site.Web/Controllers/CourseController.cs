﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Repositories;
using Site.Core.Infrastructures.DTO;
using Site.Web.Infrastructures.Attributes;
using Site.Web.Models;
using Site.Web.Models.HomeViewModel;
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

        public async Task<IActionResult> Index(Models.CourseViewModel.IndexViewModel model, CancellationToken cancellationToken)
        {
            model.Paging.PagedResult = await courseRepository.GetPagedCourseAsync(model.Searchkeyvalue, false, 2, model.PageNumber, cancellationToken,
                model.Paging.SearchParameter.PriceStatusType, model.Paging.SearchParameter.OrderStatusType, model.Paging.SearchParameter.StartingPrice,
                model.Paging.SearchParameter.EndOfPrice, model.Paging.SearchParameter.CourseGroups.Where(g => g.Checked == true).Select(g => g.Id));

            List<CourseGroupVm> CourseGroups = await courseGroupRepository.NoTrackEntities.Select(g => new CourseGroupVm { Id = g.Id, Checked = false, Title = g.Title }).ToListAsync(cancellationToken);

            model.Paging.SearchParameter.CourseGroups = CourseGroups;

            return View(model);
        }

        [AjaxOnly]
        public async Task<IActionResult> LiveSearch(Site.Web.Models.CourseViewModel.IndexViewModel model, CancellationToken cancellationToken)
        {
            //var Result = await courseRepository.GetPagedCourseAsync(model.Searchkeyvalue, false, 2, model.PageNumber, cancellationToken,model.Paging.SearchParameter.PriceStatusType, model.Paging.SearchParameter.OrderStatusType, model.Paging.SearchParameter.StartingPrice, model.Paging.SearchParameter.EndOfPrice, model.Paging.SearchParameter.CourseGroups.Where(g => g.Checked == true).Select(g => g.Id)),SearchParameter = model.Paging.SearchParameter

            PagedResult<Core.Domain.Entities.Course> result = await courseRepository.GetPagedCourseAsync(model.Searchkeyvalue, false, 2, model.PageNumber, cancellationToken,
                model.Paging.SearchParameter.PriceStatusType, model.Paging.SearchParameter.OrderStatusType, model.Paging.SearchParameter.StartingPrice,
                model.Paging.SearchParameter.EndOfPrice, model.Paging.SearchParameter.CourseGroups.Where(g => g.Checked == true).Select(g => g.Id));
            
            return PartialView("_PagedCourselistPartialView", result);
        }

    }
}