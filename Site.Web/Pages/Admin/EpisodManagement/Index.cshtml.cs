using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories;
using Site.Web.Models.PagesModels.CourseEpisodManageModel;

namespace Site.Web.Pages.Admin.EpisodManagement
{
    public class IndexModel : PageModel
    {
        public IndexModel(ICourseEpisodRepository CourseEpisodRepository, IMapper Mapper)
        {
            this.courseEpisodRepository = CourseEpisodRepository;
            this.mapper = Mapper;
            this.Model = new EpisodIndextVm();
        }

        private readonly ICourseEpisodRepository courseEpisodRepository;
        private readonly IMapper mapper;

        public EpisodIndextVm Model { get; set; }

        public async Task OnGetAsync(int CourseId, CancellationToken cancellationToken)
        {
            Model.Episods =mapper.Map<List<EpisodFullBaseVm>>(await courseEpisodRepository.GetCoursesAsync(CourseId, cancellationToken));
            Model.CourseId = CourseId;
        }
    }
}