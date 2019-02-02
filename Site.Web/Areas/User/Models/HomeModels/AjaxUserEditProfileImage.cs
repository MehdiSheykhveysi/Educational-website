using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Areas.User.Models.HomeModels
{
    public class AjaxUserEditProfileImage
    {
        [Display(Name = "آواتار")]
        [FileVerifyExtensions(fileExtensions: "jpg,jpeg,png,gif", ErrorMessage = "لطفا یک فایل با فرمت صحیح انتخاب کنید")]
        public IFormFile FormFile { get; set; }
    }
}
