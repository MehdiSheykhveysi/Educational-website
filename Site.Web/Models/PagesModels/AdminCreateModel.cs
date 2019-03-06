using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels
{
    public class AdminCreateModel
    {
        [Display(Name ="نام کاربری")]
        [Required(ErrorMessage ="{0}الزامی است")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0}الزامی است")]
        public string Email { get; set; }

        [Display(Name = "پسوورد")]
        [Required(ErrorMessage = "{0}الزامی است")]
        public string PassWord { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "{0}الزامی است")]
        public string PhoneNumber { get; set; }

        [Display(Name = "فعالسازی")]
        public bool IsActive { get; set; }

        [Display(Name = "آواتار")]
        [FileVerifyExtensions(fileExtensions: "jpg,jpeg,png,gif", ErrorMessage = "لطفا یک فایل با فرمت صحیح انتخاب کنید")]
        public IFormFile FormFile { get; set; }
    }
}
