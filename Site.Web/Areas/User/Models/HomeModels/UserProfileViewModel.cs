using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures.CustomValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Areas.User.Models.HomeModels
{
    public class UserProfileViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [MinLength(4, ErrorMessage = "نام کاربری حداقل باید چهار حرف باشد")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "{0} معتبر نیس")]
        public string Email { get; set; }

        [Display(Name = "شماره تلفن")]
        [StringLength(11, ErrorMessage = "طول {0} معتبر نیس")]
        public string PhoneNumber { get; set; }

        public string RegisterDate { get; set; }

        public decimal Wallet { get; set; }

        public string UserProfileUrl { get; set; }
    }
}
