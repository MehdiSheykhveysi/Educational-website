using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures;
using Site.Web.Infrastructures.CustomValidationAttribute;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels.CourseManageModel
{
    public class CourseBaseVieModel
    {
        public CourseBaseVieModel()
        {
            this.CourseStatuses = new List<CourseStatusVm>();
            this.CourseLevels = new List<CourseLevelVm>();
            this.CourseGroups = new List<CourseGroupVm>();
            this.CustomUsers = new List<CustomUserVm>();
        }

        [Display(Name = "وضعیت دوره")]
        [Required(ErrorMessage = "مشخص کردن وضعیت دوره اجباری است")]
        public virtual int? CourseStatusId { get; set; }
        public List<CourseStatusVm> CourseStatuses { get; set; }

        [Display(Name = "سطح دوره")]
        [Required(ErrorMessage = "انتخاب سطح دوره اجباری است")]
        public virtual int? CourseLevelId { get; set; }
        public List<CourseLevelVm> CourseLevels { get; set; }

        [Display(Name = "انتخاب مدرس ")]
        [Required(ErrorMessage = "انتخاب مدرس دوره اجباری است")]
        public virtual string CustomUserId { get; set; }
        public List<CustomUserVm> CustomUsers { get; set; }

        [Display(Name = "گروه آموزشی ")]
        [Required(ErrorMessage = "انتخاب گروه آموزشی دوره اجباری است")]
        public virtual int? CourseGroupId { get; set; }
        public List<CourseGroupVm> CourseGroups { get; set; }


    }

    public class CourseFullBaseModel : CourseBaseVieModel
    {
        public CourseFullBaseModel() : base()
        {

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
        [CheckFile(MimeTypes: "image/jpeg,image/jpeg,image/svg+xml,image/png", UploadedType: FileUploadedType.Image, ErrorMessage = "لطفا یک فایل با فرمت صحیح انتخاب کنید")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public IFormFile UploadedImage { get; set; }

        [Display(Name = "فیلم دمو دوره")]
        [CheckFile(MimeTypes: "video/x-matroska,video/mpeg,video/mpeg,video/mp4", UploadedType: FileUploadedType.Video, ErrorMessage = "لطفا یک فایل با فرمت صحیح انتخاب کنید")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است")]
        public IFormFile DemoFile { get; set; }

        [Display(Name = "کلمات کلیدی دوره")]
        [Required(ErrorMessage = "انتخاب کلمات کلیدی دوره اجباری است")]
        public List<string> Keywords { get; set; }
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