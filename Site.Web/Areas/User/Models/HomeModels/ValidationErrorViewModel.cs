using System.Collections.Generic;

namespace Site.Web.Areas.User.Models.HomeModels
{
    public class ValidationErrorViewModel
    {
        public string Status { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
