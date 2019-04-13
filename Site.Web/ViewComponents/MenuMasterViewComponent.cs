using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.ViewComponents
{
    [ViewComponent]
    public class MenuMasterViewComponent : ViewComponent
    {
        public MenuMasterViewComponent(ICourseGroupRepository CourseGroupRepository)
        {
            this.courseGroupRepository = CourseGroupRepository;
        }
        private readonly ICourseGroupRepository courseGroupRepository;

        public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
        {
            List<CourseGroup> menuList = await courseGroupRepository.Entities.Include(c=>c.ParentCourseGroup).ToListAsync(cancellationToken);
            
            return View("Default", menuList);
        }
    }
}
