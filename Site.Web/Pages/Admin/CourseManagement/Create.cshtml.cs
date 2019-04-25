using System;
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
using Site.Web.Infrastructures.Interfaces;
using Site.Web.Models.PagesModels.CourseManageModel;

namespace Site.Web.Pages.Admin.CourseManagement
{
    public class CreateModel : PageModel
    {
        public CreateModel(IMapper Mapper, IFileHandler FileHandler, IHostingEnvironment HostingEnvironment, ICourseRepository CourseRepository)
        {
            this.mapper = Mapper;
            this.fileHandler = FileHandler;
            this.hostingEnvironment = HostingEnvironment;
            this.courseRepository = CourseRepository;
        }
        private readonly IMapper mapper;
        private readonly IFileHandler fileHandler;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ICourseRepository courseRepository;

        [BindProperty]
        public CourseCreateVm Model { get; set; } = new CourseCreateVm();

        public void OnGet()
        {
        }
        
        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            Course course = new Course();
            course = mapper.Map(Model,course);
            course.CreateDate = DateTime.Now;
            string imageUploadpath = Path.Combine(hostingEnvironment.WebRootPath, "images", "CourseImages");
            string imgNewFileName = await fileHandler.UploadFileAsync(Model.UploadedImage, imageUploadpath, cancellationToken);

            fileHandler.CreateImageThumb(Path.Combine(imageUploadpath, imgNewFileName), Path.Combine(imageUploadpath, "CourseImageThumb", imgNewFileName), 120);

            string videoUploadPath = Path.Combine(hostingEnvironment.WebRootPath, "CourseDemo");
            string videoNewFileName = await fileHandler.UploadFileAsync(Model.DemoFile, videoUploadPath, cancellationToken);

            course.ImageName = imgNewFileName;
            course.DemoFileName = videoNewFileName;

            course.Keywordkeys = Model.Keywords.Select(k => new Keyword
            {
                Title = k
            }).ToList();
            await courseRepository.AddAsync(course, cancellationToken);
            return RedirectToPage("/Admin/CourseManagement/Index");
        }
    }
}