using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using Site.Web.Models.PagesModels.CourseEpisodManageModel;

namespace Site.Web.Pages.Admin.EpisodManagement
{
    public class DetailModel : PageModel
    {
        public DetailModel(ICourseEpisodRepository CourseEpisodRepository)
        {
            this.courseEpisodRepository = CourseEpisodRepository;
            this.Model = new EpisodDetailVm();
        }

        private readonly ICourseEpisodRepository courseEpisodRepository;
        [BindProperty]
        public EpisodDetailVm Model { get; set; }

        public async Task OnGetAsync(int Id, int CourseId, CancellationToken cancellationToken)
        {
            Model.CourseId = CourseId;
            CourseEpisod result = await courseEpisodRepository.GetByIdAsync(Id, cancellationToken);
            Model.IsFree = result.IsFree;
            Model.Title = result.Title;
            Model.FileName = result.FileName;
            Model.Id = result.Id;
        }
    }
}