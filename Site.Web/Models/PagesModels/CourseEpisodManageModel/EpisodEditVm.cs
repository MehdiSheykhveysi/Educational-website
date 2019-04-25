using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures;
using Site.Web.Infrastructures.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels.CourseEpisodManageModel
{
    public class EpisodEditVm : EpisodFullBaseVm
    {
        [Display(Name = "فیلم این قسمت")]
        [CheckFile(MimeTypes: "video/x-matroska,video/mpeg,video/mpeg,video/mp4", UploadedType: FileUploadedType.Video, ErrorMessage = "لطفا یک فایل با فرمت صحیح انتخاب کنید")]
        public IFormFile FormFile { get; set; }
    }
}
