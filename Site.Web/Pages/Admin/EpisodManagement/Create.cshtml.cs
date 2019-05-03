using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using Site.Web.Infrastructures.BusinessObjects;
using Site.Web.Infrastructures.Interfaces;
using Site.Core.Infrastructures.Utilities.Extensions;
using Site.Web.Models.PagesModels.CourseEpisodManageModel;

namespace Site.Web.Pages.Admin.EpisodManagement
{
    public class CreateModel : PageModel
    {
        public CreateModel(ICourseEpisodRepository CourseEpisodRepository, ICourseRepository CourseRepository, IHostingEnvironment HostingEnvironment, IFileHandler FileHandler)
        {
            this.courseEpisodRepository = CourseEpisodRepository;
            this.courseRepository = CourseRepository;
            this.fileHandler = FileHandler;
            this.hostingEnvironment = HostingEnvironment;
            this.Model = new EpisodCreateVm();
        }
        private readonly ICourseEpisodRepository courseEpisodRepository;
        private readonly ICourseRepository courseRepository;
        private readonly IFileHandler fileHandler;
        private readonly IHostingEnvironment hostingEnvironment;

        [BindProperty]
        public EpisodCreateVm Model { get; set; }

        public void OnGet(int CourseId)
        {
            Model.CourseId = CourseId;
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            CourseEpisod episod = new CourseEpisod
            {
                IsFree = Model.IsFree,
                Title = Model.Title
            };
            //Save File
            string videoUploadPath = Path.Combine(hostingEnvironment.WebRootPath, "CourseDemo", "EpisodVideo");
            string videoNewFileName = await fileHandler.UploadFileAsync(Model.FormFile, videoUploadPath, cancellationToken);

            //Create Thumbnail Path
            var videofilepath = Path.Combine(videoUploadPath, videoNewFileName);
            string outPutPath = Path.Combine(hostingEnvironment.WebRootPath, "images", "CourseImages", "BigEpisodThump", videoNewFileName.ChangeExtension(".jpg"));

            //Create Thumbnail Path From Video
            FileInformation fileinfo = await fileHandler.GetThumonailFromVideoAsync(videofilepath, outPutPath, cancellationToken);

            string SaveThumnailPath = Path.Combine(hostingEnvironment.WebRootPath, "images", "CourseImages", "MainEpisodThump", videoNewFileName.ChangeExtension(".jpg"));
            fileHandler.CreateImageThumb(outPutPath, SaveThumnailPath, 90);

            fileHandler.DeleteOldImageThumb(outPutPath, string.Empty);

            episod.EpisodeTime = fileinfo.MetaData.Duration;

            episod.FileName = videoNewFileName;

            Course course = await courseRepository.GetByIdAsync(Model.CourseId, cancellationToken);
            course.TotalEpisodTime += episod.EpisodeTime;
            episod.Course = course;
            await courseEpisodRepository.AddAsync(episod, cancellationToken);

            return RedirectToPage("/Admin/EpisodManagement/Index", new { Model.CourseId });
        }
    }
}