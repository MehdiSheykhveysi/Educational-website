using Microsoft.AspNetCore.Mvc;
using Site.Core.DataBase.Repositories;
using Site.Web.Models.HomeViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ICourseRepository CourseRepository)
        {
            this.courseRepository = CourseRepository;
        }

        private readonly ICourseRepository courseRepository;

        public async Task<IActionResult> Index(IndexViewModel model, CancellationToken cancellationToken)
        {
            model.PagedResult = await courseRepository.GetPagedCourseAsync(model.Searchkeyvalue, false, 8, model.PageNumber, cancellationToken);
            
            return View(model);
        }
    }
}