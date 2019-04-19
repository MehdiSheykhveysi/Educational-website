using Site.Core.Infrastructures.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels.CourseManageModel
{
    public class CourseIndexVm
    {
        public CourseIndexVm()
        {
            this.PagedResult = new PagedResult<CourseVm>();
        }
        public PagedResult<CourseVm>  PagedResult { get; set; }
        public bool IsDeleted { get; set; }
        public string Searckkeyvalue { get; set; }
    }
    public class CourseVm
    {
        public int Id { get; set; }
        [Display(Name ="عنوان دوره")]
        public string CourseTitle { get; set; }

        [Display(Name ="قیمت دوره")]
        public decimal CoursePrice { get; set; }

        [Display(Name ="تصویر")]
        public string ImageName { get; set; }

        [Display(Name ="تاریخ شروع دوره")]
        public DateTime CreateDate { get; set; }
    }
}
