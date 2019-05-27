using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Repositories;
using Site.Core.DataBase.Repositories.CustomizeIdentity;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.DTO;
using Site.Web.Infrastructures.Attributes;
using Site.Web.Infrastructures.Compares;
using Site.Web.Models.CourseViewModel;
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
        public CourseController(ICourseGroupRepository CourseGroupRepository, CustomUserManager UserManager, ICourseRepository CourseRepository, IOrderRepository OrderRepository, ICommentRepository CommentRepository)
        {
            this.courseGroupRepository = CourseGroupRepository;
            this.courseRepository = CourseRepository;
            this.orderRepository = OrderRepository;
            this.commentRepository = CommentRepository;
            userManager = UserManager;
        }
        private readonly ICourseRepository courseRepository;
        private readonly ICourseGroupRepository courseGroupRepository;
        private readonly IOrderRepository orderRepository;
        private readonly ICommentRepository commentRepository;
        private readonly CustomUserManager userManager;

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
                    else
                        g.Checked = false;
                });
            }
            model.Paging.SearchParameter.CourseGroups.Clear();
            model.Paging.SearchParameter.CourseGroups = CourseGroups;

            return View(model);
        }

        [AjaxOnly]
        public async Task<IActionResult> LiveSearch(Models.CourseViewModel.IndexViewModel model, CancellationToken cancellationToken)
        {
            PagedResult<Core.Domain.Entities.Course> result = await courseRepository.GetPagedCourseAsync(model.Searchkeyvalue, false, 2, model.PageNumber, cancellationToken,
                model.Paging.SearchParameter.PriceStatusType, model.Paging.SearchParameter.OrderStatusType, model.Paging.SearchParameter.StartingPrice,
                model.Paging.SearchParameter.EndOfPrice, model.Paging.SearchParameter.CourseGroups.Where(g => g.Checked == true).Select(g => g.Id));

            return PartialView("_PagedCourselistPartialView", result);
        }

        public async Task<IActionResult> Detail(int CourseId, CancellationToken cancellationToken)
        {
            CourseDetailVm model = new CourseDetailVm
            {
                CourseDetail = await courseRepository.GetCourseDetailAsync(CourseId, cancellationToken)
            };

            if (model.CourseDetail == null)
                return NotFound();
            else
            {
                model.PagedComment = await commentRepository.GetPagedComment(CourseId, 1,5, cancellationToken);
                model.CourseDetail.OrderCount = await orderRepository.GetOrderedCountAsync(CourseId, cancellationToken);
            }

            return View(model);

        }

        public async Task<IActionResult> GetComments(int CourseId, CancellationToken cancellationToken, int CurrentPageNumber = 1, int TakeCount = 5)
        {
            PagedResult<CommentDTO> comments = await commentRepository.GetPagedComment(CourseId, CurrentPageNumber, TakeCount, cancellationToken);

            return PartialView("CommentListPartialView", comments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CourseCreateCommnet model, CancellationToken cancellationToken)
        {
            CustomUser user = await userManager.GetUserAsync(User);
            Comment comment = new Comment
            {
                Body = model.Body,
                CoourseId = model.CourseId,
                IsDeleted = false,
                IsReadedByAdmin = false,
                Name = user.ShowUserName,
                User = user,
                UserId = user.Id,
                CreateTime = DateTime.Now
            };

            await commentRepository.AddAsync(comment, cancellationToken);

            return RedirectToAction(nameof(GetComments), new { model.CourseId, model.CurrentPageNumber });
        }
    }
}