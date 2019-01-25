using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.AccountModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [Display(Name = "ایمیل")]
        public string Username { get; set; }

        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        [Display(Name = "شماره تلفن")]
        [StringLength(11)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "پسوورد")]
        [UIHint("Password")]
        public string Password { get; set; }

        [UIHint("Password")]
        [Compare(nameof(Password))]
        [Display(Name = "تکرار پسوورد")]
        [Required(ErrorMessage = "لطفا تکرار {0} را وارد کنید")]
        public string ConfirmPassword { get; set; }
        public string ReturnUrl { get; set; } = "";
    }
}
