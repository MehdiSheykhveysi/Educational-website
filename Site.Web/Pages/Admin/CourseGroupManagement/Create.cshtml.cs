using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories;
using Site.Web.Models.PagesModels.CourseGroupModel;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Pages.Admin.CourseGroupManagement
{
    public class CreateModel : PageModel
    {

        private readonly ICourseGroupRepository courseGroupRepository;

        public CreateModel(ICourseGroupRepository CourseGroupRepository)
        {
            courseGroupRepository = CourseGroupRepository;
        }

        [BindProperty]
        public CreateVm Model { get; set; }

        public void OnGet(int? ParentId)
        {
            Model = new CreateVm
            {
                ParentId = ParentId
            };
        }

        public async Task<IActionResult> OnPostAsync(CreateVm model, CancellationToken cancellationToken)
        {

            await courseGroupRepository.AddAsync(new Core.Domain.Entities.CourseGroup { ParentId = model.ParentId, Title = model.GroupTitle, IsDeleted = false }, cancellationToken);

            return Redirect("/Admin/CourseGroupManagement/Index");
        }
    }
}