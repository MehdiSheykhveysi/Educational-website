using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using Site.Web.Models.PagesModels.CourseManageModel;

namespace Site.Web.Pages.Admin.CourseManagement
{
    public class DeleteModel : PageModel
    {
        public DeleteModel(ICourseRepository CourseRepository, IMapper Mapper)
        {
            this.courseRepository = CourseRepository;
            this.mapper = Mapper;
            this.Model = new CourseDeleteVm();
        }

        private readonly ICourseRepository courseRepository;
        private readonly IMapper mapper;

        public CourseDeleteVm Model { get; set; }

        public async Task OnGetAsync(int Id, CancellationToken cancellationToken)
        {
            Course course = await courseRepository.GetCourseWithKeyWordsAsync(Id, cancellationToken);
            Model = mapper.Map<CourseDeleteVm>(course);
            Model.Tags = string.Join(',', course.Keywordkeys.Select(k => k.Title));
        }

        public async Task<IActionResult> OnPostAsync(int Id, CancellationToken cancellationToken)
        {
            Course course = await courseRepository.GetByIdAsync(Id, cancellationToken);
            course.UpdateDate = DateTime.Now;
            course.IsDeleted = true;
            await courseRepository.UpdateAsync(course, cancellationToken);
            return RedirectToPage("/Admin/CourseManagement/Index");
        }
    }
}