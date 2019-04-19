using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Repositories;
using Site.Core.DataBase.Repositories.CustomizeIdentity;
using Site.Core.Domain.Entities;
using Site.Web.Models.PagesModels.CourseManageModel;
using Site.Web.Models.ViewComponentViewModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.ViewComponents
{
    public class FillCourseDetailViewComponent : ViewComponent
    {
        public FillCourseDetailViewComponent(ICourseRepository CourseRepository,
            ICourseGroupRepository CourseGroupRepository,
            ICourseLevelRepository CourseLevelRepository,
            ICourseStatusRepositoty CourseStatusRepositoty,
            IKeywordRepository KeywordRepository,
            IMapper Mapper,
            CustomUserManager CustomUserManager)
        {
            this.courseRepository = CourseRepository;
            this.courseGroupRepository = CourseGroupRepository;
            this.courseLevelRepository = CourseLevelRepository;
            this.courseStatusRepositoty = CourseStatusRepositoty;
            this.keywordRepository = KeywordRepository;
            this.customUserManager = CustomUserManager;
            this.mapper = Mapper;
            this.model = new FillCourseDetaliVm();
        }

        private readonly ICourseStatusRepositoty courseStatusRepositoty;
        private readonly ICourseRepository courseRepository;
        private readonly ICourseGroupRepository courseGroupRepository;
        private readonly ICourseLevelRepository courseLevelRepository;
        private readonly IKeywordRepository keywordRepository;
        private readonly CustomUserManager customUserManager;
        private readonly IMapper mapper;

        private readonly FillCourseDetaliVm model;

        public async Task<IViewComponentResult> InvokeAsync(int CourseGroupId, int CourseLevelId, int CourseStatusId, string CustomUserId, CancellationToken cancellationToken)
        {
            List<CourseGroup> groups = await courseGroupRepository.NoTrackEntities.ToListAsync(cancellationToken);
            model.CourseGroups = mapper.Map<List<CourseGroupVm>>(groups);
            model.CourseGroupId = CourseGroupId;

            List<CourseStatus> Status = await courseStatusRepositoty.NoTrackEntities.ToListAsync(cancellationToken);
            model.CourseStatuses = mapper.Map<List<CourseStatusVm>>(Status);
            model.CourseStatusId = CourseStatusId;

            List<CourseLevel> levels = await courseLevelRepository.NoTrackEntities.ToListAsync(cancellationToken);
            model.CourseLevels = mapper.Map<List<CourseLevelVm>>(levels);
            model.CourseLevelId = CourseLevelId;

            IList<CustomUser> Masters = await customUserManager.GetUsersInRoleAsync("Master");
            model.CustomUsers = mapper.Map<List<CustomUserVm>>(Masters);
            model.CustomUserId = CustomUserId;

            return View("Default", model: model);
        }
    }
}
