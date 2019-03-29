using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Site.Web.Infrastructures.CustomValidationAttribute;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Areas.User.Models.HomeModels
{
    public class AjaxUserEditProfileImage
    {
        [Display(Name = "آواتار")]
        [FileVerifyExtensions(fileExtensions: "jpg,jpeg,png,gif", ErrorMessage = "لطفا یک فایل با فرمت صحیح انتخاب کنید")]
        [Required(ErrorMessage ="لطفا یک عکس انتخاب کنید")]
        public IFormFile FormFile { get; set; }

        [ReadOnly(true)]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }
    }
}
