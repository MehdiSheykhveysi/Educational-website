using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures.CustomValidationAttribute;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels.CourseManageModel
{
    public class CourseCreateVm
    {
        public CourseCreateVm()
        {
            this.CourseStatuses = new List<CourseStatusVm>();
            this.CourseLevels = new List<CourseLevelVm>();
            this.CourseGroups = new List<CourseGroupVm>();
            this.CustomUsers = new List<CustomUserVm>();
        }

        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string CourseTitle { get; set; }

        [Display(Name = "توضیحات دوره")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public string CourseDescription { get; set; }

        [Display(Name = "قیمت دوره")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public decimal CoursePrice { get; set; }

        [Display(Name = "عکس دوره")]
        [FileVerifyExtensions(fileExtensions: "jpg,jpeg,png,gif", ErrorMessage = "لطفا یک فایل با فرمت صحیح انتخاب کنید")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public IFormFile UploadedImage { get; set; }

        [Display(Name = "فیلم دمو دوره")]
        [FileVerifyExtensions(fileExtensions: "mp4,mkv", ErrorMessage = "لطفا یک فایل با فرمت صحیح انتخاب کنید")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public IFormFile DemoFileName { get; set; }

        [Display(Name = "مشخص کردن وضعیت دوره")]
        [Required(ErrorMessage = "مشخص کردن وضعیت دوره اجباری است")]
        public int? CourseStatusId { get; set; }
        public List<CourseStatusVm> CourseStatuses { get; set; }

        [Display(Name = "انتخاب سطح دوره")]
        [Required(ErrorMessage = "انتخاب سطح دوره اجباری است")]
        public int? CourseLevelId { get; set; }
        public List<CourseLevelVm> CourseLevels { get; set; }

        [Display(Name = "انتخاب مدرس دوره")]
        [Required(ErrorMessage = "انتخاب مدرس دوره اجباری است")]
        public string CustomUserId { get; set; }
        public List<CustomUserVm> CustomUsers { get; set; }

        [Display(Name = "گروه آموزشی دوره")]
        [Required(ErrorMessage = "انتخاب گروه آموزشی دوره اجباری است")]
        public int? CourseGroupId { get; set; }
        public List<CourseGroupVm> CourseGroups { get; set; }

        [Display(Name = "کلمات کلیدی دوره")]
        [Required(ErrorMessage = "انتخاب کلمات کلیدی دوره اجباری است")]
        public List<string> Keywordkeys { get; set; }
    }

    public class CourseStatusVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class CourseLevelVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class CustomUserVm
    {
        public string Id { get; set; }
        public string ShowUserName { get; set; }
    }
    public class CourseGroupVm
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }
}
