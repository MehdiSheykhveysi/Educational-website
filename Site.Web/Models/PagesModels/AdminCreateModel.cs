using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels
{
    public class AdminCreateModel : BaseViewModel
    {
        [Display(Name = "پسوورد")]
        [Required(ErrorMessage = "{0}الزامی است")]
        public virtual string PassWord { get; set; }   
    }
}
