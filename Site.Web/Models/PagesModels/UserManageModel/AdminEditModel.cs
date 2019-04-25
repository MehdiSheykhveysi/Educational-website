using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Site.Web.Infrastructures;
using Site.Web.Infrastructures.CustomValidationAttribute;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels
{
    public class AdminEditModel : FullBaseViewModel
    {
        [HiddenInput(DisplayValue = false)]
        [ReadOnly(true)]
        public string Id { get; set; }

        public string Avatar { get; set; }

        [Display(Name = "آواتار")]
        [CheckFile(MimeTypes: "image/jpeg,image/jpeg,image/svg+xml,image/png", UploadedType: FileUploadedType.Image, ErrorMessage = "لطفا یک فایل با فرمت صحیح انتخاب کنید")]
        public override IFormFile FormFile { get => base.FormFile; set => base.FormFile = value; }
    }
}