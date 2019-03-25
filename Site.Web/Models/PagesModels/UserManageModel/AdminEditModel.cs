using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Site.Web.Infrastructures.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels
{
    public class AdminEditModel : FullBaseViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        public string Avatar { get; set; }

        [Display(Name = "آواتار")]
        [FileVerifyExtensions(fileExtensions: "jpg,jpeg,png,gif", ErrorMessage = "لطفا یک فایل با فرمت صحیح انتخاب کنید")]
        public override IFormFile FormFile { get => base.FormFile; set => base.FormFile = value; }
    }
}