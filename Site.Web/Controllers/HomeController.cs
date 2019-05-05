using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using Site.Web.Models.HomeViewModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ICourseRepository CourseRepository, ICourseGroupRepository CourseGroupRepository)
        {
            this.courseRepository = CourseRepository;
            this.courseGroupRepository = CourseGroupRepository;
        }

        private readonly ICourseRepository courseRepository;
        private readonly ICourseGroupRepository courseGroupRepository;

        public async Task<IActionResult> Index(IndexViewModel model, CancellationToken cancellationToken)
        {
            model.PagedResult = await courseRepository.GetPagedCourseAsync(model.Searchkeyvalue, false, 8, model.PageNumber, cancellationToken);

            return View(model);
        }
    }
}