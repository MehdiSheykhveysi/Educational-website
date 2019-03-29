using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Areas.User.Models.HomeModels
{
    public class AjaxUserChangrPassword
    {
        [ReadOnly(true)]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Display(Name = " رمز ورود قدیمی ")]
        [Required(ErrorMessage = "لطفا رمز ورود قدیمی را وارد کنید")]
        public string OldPassWord { get; set; }
        
        [Display(Name = "رمز ورود جدید")]
        [Required(ErrorMessage = "لطفا رمز ورود جدید را وارد کنید")]
        public string NewPassWords { get; set; }
    }
}
