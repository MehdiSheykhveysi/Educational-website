using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.AccountModels
{
    public class LogInViewModel
    {
        [Display(Name="ایمیل")]
        [Required(ErrorMessage ="لطفا اایملتان را وارد کنید")]
        public string Email { get; set; }
        [Display(Name="پسوورد")]
        [Required(ErrorMessage = "لطفا پسوورد را وارد کنید")]
        public string PassWord { get; set; }
        [Display(Name="مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
