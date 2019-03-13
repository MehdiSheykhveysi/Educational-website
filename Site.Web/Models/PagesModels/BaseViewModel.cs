using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures.CustomValidationAttribute;

namespace Site.Web.Models.PagesModels
{
    public class BaseViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0}الزامی است")]
        public virtual string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0}الزامی است")]
        public string Email { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "{0}الزامی است")]
        public string PhoneNumber { get; set; }

        [Display(Name = "فعالسازی")]
        public bool IsActive { get; set; }

        [Display(Name = "آواتار")]
        [Required(ErrorMessage ="dfhspdpd")]
        [FileVerifyExtensions(fileExtensions: "jpg,jpeg,png,gif", ErrorMessage = "لطفا یک فایل با فرمت صحیح انتخاب کنید")]
        public IFormFile FormFile { get; set; }

        public List<RoleModel> SelectedRoles { get; set; } = new List<RoleModel>();
    }

    public class RoleModel
    {
        public string Name { get; set; }

        public bool Checked { get; set; }
    }
}