using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels.DisCountModel
{
    public class Detail : DisCountBaseVmModel
    {
        [Required]
        public int Id { get; set; }
    }
}
