using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories;
using Site.Core.Infrastructures.DTO;
using Site.Web.Models.CourseViewModel;

namespace Site.Web.Pages.Admin.CourseGroupManagement
{
    public class IndexModel : PageModel
    {
        private readonly ICourseGroupRepository courseGroupRepository;

        public IndexModel(ICourseGroupRepository CourseGroupRepository)
        {
            courseGroupRepository = CourseGroupRepository;
            GroupIndexSearch = new GroupIndexSearchModel();
        }

        public List<CourseGroupDTO> Model { get; set; }

        [BindProperty]
        public GroupIndexSearchModel GroupIndexSearch { get; set; }

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        {
            Model = await courseGroupRepository.GetGroupWithSubGroupAsync(cancellationToken, GroupIndexSearch.GroupName, GroupIndexSearch.IsDeleted);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            Model = await courseGroupRepository.GetGroupWithSubGroupAsync(cancellationToken, GroupIndexSearch.GroupName, GroupIndexSearch.IsDeleted);
            return Page();
        }
    }
}