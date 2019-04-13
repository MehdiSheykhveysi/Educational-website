using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories;
using Site.Web.Models.PagesModels.CourseManageModel;

namespace Site.Web.Pages.Admin.CourseManagement
{
    public class IndexModel : PageModel
    {
        public IndexModel(ICourseRepository CourseRepository)
        {
            this.CourseRepository = CourseRepository;
        }

        private readonly ICourseRepository CourseRepository;
        [BindProperty]
        public CourseCreateVm Model { get; set; } 

        public void OnGet()
        {
            //await CourseRepository.GetByIdAsync(1, cancellationToken);
        }
        
        public void OnPost()
        {
        }
    }
}