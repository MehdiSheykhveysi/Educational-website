using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.AccountModels
{
    public class CheckEmailViewModel
    {
        [EmailAddress(ErrorMessage ="ایمیل شما اشتباه است")]
        [StringLength(300)]
        [Required(ErrorMessage ="ایمیل خورا باید وارد کنید")]
        public string Email { get; set; }
    }
}
