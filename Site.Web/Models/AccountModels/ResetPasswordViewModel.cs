using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.AccountModels
{
    public class ResetPasswordViewModel
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage ="لطفا پسوورد را وارد کنید")]
        [DataType("Password")]
        public string NewPassword { get; set; }
    }
}