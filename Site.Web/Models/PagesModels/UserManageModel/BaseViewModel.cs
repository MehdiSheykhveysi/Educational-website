using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Site.Web.Models.PagesModels
{
    public class BaseViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0}الزامی است")]
        public string Email { get; set; }

        [Display(Name = " وضعیت فعالسازی")]
        public bool IsActive { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "{0}الزامی است")]
        public string PhoneNumber { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0}الزامی است")]
        public string ShowUserName { get; set; }
    }

    public class WithFileBaseViewModel : BaseViewModel
    {
        public virtual IFormFile FormFile { get; set; }
    }
}