using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels.DisCountModel
{
    public class DisCountEditVm : DisCountBaseVmModel
    {
        [Required]
        public int Id { get; set; }
    }
}
