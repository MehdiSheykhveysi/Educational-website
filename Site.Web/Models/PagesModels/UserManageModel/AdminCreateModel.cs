using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures;
using Site.Web.Infrastructures.CustomValidationAttribute;

namespace Site.Web.Models.PagesModels
{
    public class AdminCreateModel : FullBaseViewModel
    {
        [Display(Name = "پسوورد")]
        [Required(ErrorMessage = "{0}الزامی است")]
        public virtual string PassWord { get; set; }

        [Display(Name = "آواتار")]
        [CheckFile(MimeTypes: "image/jpeg,image/jpeg,image/svg+xml,image/png", UploadedType: FileUploadedType.Image, ErrorMessage = "لطفا یک فایل با فرمت صحیح انتخاب کنید")]
        public override IFormFile FormFile { get => base.FormFile; set => base.FormFile = value; }
    }
}
