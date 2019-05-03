using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using Site.Web.Infrastructures.Interfaces;
using Site.Core.Infrastructures.Utilities.Extensions;

namespace Site.Web.Pages.Admin.EpisodManagement
{
    public class DeleteModel : PageModel
    {
        public DeleteModel(ICourseEpisodRepository CourseEpisodRepository, IHostingEnvironment HostingEnvironment, IFileHandler FileHandler)
        {
            this.courseEpisodRepository = CourseEpisodRepository;
            this.hostingEnvironment = HostingEnvironment;
            this.fileHandler = FileHandler;
        }

        private readonly ICourseEpisodRepository courseEpisodRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IFileHandler fileHandler;

        public async Task OnGetAsync(int Id, int CourseId, CancellationToken cancellationToken)
        {
            CourseEpisod episod = await courseEpisodRepository.GetByIdAsync(Id, cancellationToken);
            ViewData["CourseId"] = CourseId;

            //Create video path
            string videoUploadPath = Path.Combine(hostingEnvironment.WebRootPath, "CourseDemo", "EpisodVideo", episod.FileName);

            //Delete Old video Path
            fileHandler.DeleteOldImageThumb(videoUploadPath, string.Empty);

            //Delete Old Big Thumbnail
            fileHandler.DeleteOldImageThumb(Path.Combine(hostingEnvironment.WebRootPath, "images", "CourseImages", "BigEpisodThump", episod.FileName.ChangeExtension(".jpg")), string.Empty);

            //Delete Old Main Thumbnail
            fileHandler.DeleteOldImageThumb(Path.Combine(hostingEnvironment.WebRootPath, "images", "CourseImages", "MainEpisodThump", episod.FileName.ChangeExtension(".jpg")), string.Empty);

            await courseEpisodRepository.DeleteAsync(episod, cancellationToken);
        }
    }
}