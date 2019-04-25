using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.Utilities;
using Site.Web.Infrastructures.Interfaces;
using Site.Web.Models.PagesModels.CourseEpisodManageModel;
using Site.Core.Infrastructures.Utilities.Extensions;
using Site.Web.Infrastructures.BusinessObjects;

namespace Site.Web.Pages.Admin.EpisodManagement
{
    public class EditModel : PageModel
    {
        public EditModel(ICourseEpisodRepository CourseEpisodRepository, IHostingEnvironment HostingEnvironment, IFileHandler FileHandler)
        {
            this.courseEpisodRepository = CourseEpisodRepository;
            this.hostingEnvironment = HostingEnvironment;
            this.fileHandler = FileHandler;
            this.Model = new EpisodEditVm();
        }

        private readonly ICourseEpisodRepository courseEpisodRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IFileHandler fileHandler;
        [BindProperty]
        public EpisodEditVm Model { get; set; }

        public async Task OnGetAsync(int Id, int CourseId, CancellationToken cancellationToken)
        {
            Model.CourseId = CourseId;
            CourseEpisod result = await courseEpisodRepository.GetByIdAsync(Id, cancellationToken);
            Model.IsFree = result.IsFree;
            Model.Title = result.Title;
            Model.FileName = result.FileName;
            Model.Id = result.Id;
        }
        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            CourseEpisod episod = await courseEpisodRepository.GetByIdAsync(Model.Id, cancellationToken);
            episod.Title = Model.Title;
            episod.IsFree = Model.IsFree;
            episod.CourseId = Model.CourseId;

            if (Assert.NotNull(Model.FormFile))
            {
                //Save video
                string videoUploadPath = Path.Combine(hostingEnvironment.WebRootPath, "CourseDemo", "EpisodVideo");
                string videoNewFileName = await fileHandler.UploadFileAsync(Model.FormFile, videoUploadPath, cancellationToken,Path.Combine(videoUploadPath,episod.FileName));

                //Delete Old video Path
                fileHandler.DeleteOldImageThumb(Path.Combine(videoUploadPath,episod.FileName), string.Empty);
                
                //Delete Old Big Thumbnail
                fileHandler.DeleteOldImageThumb(Path.Combine(hostingEnvironment.WebRootPath, "images", "CourseImages", "BigEpisodThump",episod.FileName.ChangeExtension(".jpg")), string.Empty);
               
                //Delete Old Main Thumbnail
                fileHandler.DeleteOldImageThumb(Path.Combine(hostingEnvironment.WebRootPath, "images", "CourseImages", "MainEpisodThump", episod.FileName.ChangeExtension(".jpg")), string.Empty);
               
                //Create Big Thumbnail Path From Video & Create Thumbnail Path
                string outPutPath = Path.Combine(hostingEnvironment.WebRootPath, "images", "CourseImages", "BigEpisodThump", videoNewFileName.ChangeExtension(".jpg"));
                string videofilepath = Path.Combine(videoUploadPath, videoNewFileName);
                FileInformation fileinfo = await fileHandler.GetThumonailFromVideoAsync(videofilepath, outPutPath, cancellationToken);

                //Create  Main Thumbnail
                string SaveThumnailPath = Path.Combine(hostingEnvironment.WebRootPath, "images", "CourseImages", "MainEpisodThump", videoNewFileName.ChangeExtension(".jpg"));
                fileHandler.CreateImageThumb(outPutPath, SaveThumnailPath, 90);
                
                episod.EpisodeTime = fileinfo.MetaData.Duration;
                episod.FileName = videoNewFileName;
            }
            await courseEpisodRepository.UpdateAsync(episod, cancellationToken);

            return RedirectToPage("/Admin/EpisodManagement/Index", new { Model.CourseId });
        }
    }
}