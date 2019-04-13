using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Repositories;
using Site.Core.DataBase.Repositories.CustomizeIdentity;
using Site.Core.Domain.Entities;
using Site.Web.Models.PagesModels.CourseManageModel;

namespace Site.Web.Pages.Admin.CourseManagement
{
    public class CreateModel : PageModel
    {
        public CreateModel(ICourseRepository CourseRepository,
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
        }

        private readonly ICourseStatusRepositoty courseStatusRepositoty;
        private readonly ICourseRepository courseRepository;
        private readonly ICourseGroupRepository courseGroupRepository;
        private readonly ICourseLevelRepository courseLevelRepository;
        private readonly IKeywordRepository keywordRepository;
        private readonly CustomUserManager customUserManager;
        private readonly IMapper mapper;

        [BindProperty]
        public CourseCreateVm Model { get; set; } = new CourseCreateVm();

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            List<CourseGroup> groups = await courseGroupRepository.NoTrackEntities.ToListAsync(cancellationToken);
            Model.CourseGroups = mapper.Map<List<CourseGroupVm>>(groups);

            List<CourseStatus> Status = await courseStatusRepositoty.NoTrackEntities.ToListAsync(cancellationToken);
            Model.CourseStatuses = mapper.Map<List<CourseStatusVm>>(Status);

            List<CourseLevel> levels = await courseLevelRepository.NoTrackEntities.ToListAsync(cancellationToken);
            Model.CourseLevels = mapper.Map<List<CourseLevelVm>>(levels);

            IList<CustomUser> Masters = await customUserManager.GetUsersInRoleAsync("Master");
            Model.CustomUsers = mapper.Map<List<CustomUserVm>>(Masters);
        }

        //public async Task<IActionResult> OnGetKeywordlistAsync(CancellationToken cancellationToken)
        //{
        //    AjaxResult<KeywordkeyVm> result = new AjaxResult<KeywordkeyVm>("Success");

        //    List<Keywordkey> keywords = await keywordRepository.GetKeywordkeys(cancellationToken, null);
        //    result.SuccessedResultList = mapper.Map<List<KeywordkeyVm>>(keywords);

        //    return new JsonResult(result);
        //}

        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }
    }
}