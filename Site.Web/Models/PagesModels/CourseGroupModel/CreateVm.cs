using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels.CourseGroupModel
{
    public class CreateVm
    {
        public int? ParentId { get; set; }
        
        [Display(Name ="نام گروه")]
        [Required(ErrorMessage ="افزودن نام گروه اجباری است")]
        [StringLength(maximumLength:60,ErrorMessage ="حداکثر طول بایستی {0} باشد",MinimumLength =3)]
        public string GroupTitle { get; set; }
    }
}
