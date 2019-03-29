using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Site.Web.Areas.User.Models.HomeModels
{
    public class ValidationErrorViewModel
    {
        public string Status { get; set; }
        public string RedirectUrl { get; set; }
        public string MessageWhenSuccessed { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public ValidationErrorViewModel()
        {

        }

        public ValidationErrorViewModel(string Status)
        {
            this.Status = Status;
        }

        public void AddErrrs(ModelStateDictionary Modelstate)
        {
            this.Errors.AddRange(Modelstate.Values.SelectMany(c => c.Errors).Select(c => c.ErrorMessage));
        }
    }
}
