using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories;
using Site.Core.Infrastructures.DTO;
using Site.Web.Models.PagesModels.CourseManageModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Pages.Admin.CourseManagement
{
    public class IndexModel : PageModel
    {
        public IndexModel(ICourseRepository CourseRepository, IMapper Mapper)
        {
            this.CourseRepository = CourseRepository;
            this.mapper = Mapper;
            this.Model = new CourseIndexVm();
        }

        private readonly ICourseRepository CourseRepository;
        private readonly IMapper mapper;
        public CourseIndexVm Model;

        public async Task OnGetAsync(CancellationToken cancellationToken, int PageNumber = 1, bool IsDeleted = false, string Searckkeyvalue = null)
        {
            PagedResult<Core.Domain.Entities.Course> PagedResult = await CourseRepository.GetPagedCourseAsync(Searckkeyvalue, IsDeleted, 3, PageNumber, cancellationToken);
            Model.PagedResult.PageData = PagedResult.PageData;
            Model.PagedResult.ListItem = mapper.Map<List<CourseVm>>(PagedResult.ListItem);
            Model.Searckkeyvalue = Searckkeyvalue;
            Model.IsDeleted = IsDeleted;
            ViewData["SearchKey"] = Searckkeyvalue;
        }
    }
}