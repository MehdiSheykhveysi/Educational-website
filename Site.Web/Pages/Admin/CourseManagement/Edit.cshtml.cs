using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.Utilities;
using Site.Web.Infrastructures;
using Site.Web.Infrastructures.Interfaces;
using Site.Web.Models.PagesModels.CourseManageModel;

namespace Site.Web.Pages.Admin.CourseManagement
{
    public class EditModel : PageModel
    {
        public EditModel(ICourseRepository CourseRepository, IFileHandler FileHandler, IHostingEnvironment HostingEnvironment, IMapper Mapper)
        {
            this.courseRepository = CourseRepository;
            this.fileHandler = FileHandler;
            this.hostingEnvironment = HostingEnvironment;
            this.mapper = Mapper;
            this.Model = new CourseEditVm();
        }
        private readonly ICourseRepository courseRepository;
        private readonly IFileHandler fileHandler;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IMapper mapper;

        [BindProperty]
        public CourseEditVm Model { get; set; }

        public async Task OnGetAsync(int Id, CancellationToken cancellationToken)
        {
            var selectedCourse = await courseRepository.GetCourseWithKeyWordsAsync(Id, cancellationToken);
            Model = mapper.Map<CourseEditVm>(selectedCourse);
            Model.Tags = string.Join(',', selectedCourse.Keywordkeys.Select(k => k.Title));
        }
        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            Course course = await courseRepository.GetByIdAsync(Model.Id, cancellationToken);

            course = mapper.Map(Model,course);
            course.CreateDate = course.CreateDate;
            course.UpdateDate = DateTime.Now;

            if (Assert.NotNull(Model.UploadedImage))
            {
                string imageUploadpath = Path.Combine(hostingEnvironment.WebRootPath, "images", "CourseImages");
                string imgNewFileName = await fileHandler.UploadImageAsync(Model.UploadedImage, imageUploadpath, "\\images\\CourseImages\\", FileUploadedType.Image, cancellationToken, $"{imageUploadpath}\\{Model.ImageName}");

                fileHandler.DeleteOldImageThumb($"{imageUploadpath}\\CourseImageThumb\\{Model.ImageName}", Model.ImageName);
                fileHandler.CreateImageThumb(Path.Combine(imageUploadpath, imgNewFileName), Path.Combine(imageUploadpath, "CourseImageThumb", imgNewFileName), 120);

                course.ImageName = imgNewFileName;
            }

            if (Assert.NotNull(Model.DemoFile))
            {
                string videoUploadPath = Path.Combine(hostingEnvironment.WebRootPath, "CourseDemo");
                string videoNewFileName = await fileHandler.UploadImageAsync(Model.DemoFile, videoUploadPath, "\\CourseDemo\\", FileUploadedType.Video, cancellationToken, $"{videoUploadPath}\\{Model.ImageName}");
                course.DemoFileName = videoNewFileName;
            }

            course.Keywordkeys = Model.Keywords.Select(k => new Keyword
            {
                Title = k
            }).ToList();

            await courseRepository.UpdateAsync(course, cancellationToken);
            return RedirectToPage("/Admin/CourseManagement/Index");
        }
    }
}