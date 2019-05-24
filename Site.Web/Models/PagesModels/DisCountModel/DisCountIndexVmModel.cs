using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels.DisCountModel
{
    public class DisCountIndexVmModel : DisCountBaseVmModel
    {
        [Required]
        public int Id { get; set; }
    }
}
