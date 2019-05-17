using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Site.Web.Infrastructures.BusinessObjects
{
    public class AjaxResult
    {
        public string Status { get; set; }
        public string RedirectUrl { get; set; }
        public string MessageWhenSuccessed { get; set; }
        public List<string> Errors { get; set; }

        public AjaxResult() => this.Errors = new List<string>();

        public AjaxResult(string Status) : this() => this.Status = Status;

        public void AddErrors(ModelStateDictionary Modelstate) => this.Errors.AddRange(Modelstate.Values.SelectMany(c => c.Errors).Select(c => c.ErrorMessage));
    }

    public class AjaxResult<TSuccessResult> : AjaxResult
    {
        public List<TSuccessResult> SuccessedResultList { get; set; }

        public AjaxResult() : base() => this.SuccessedResultList = new List<TSuccessResult>();

        public AjaxResult(string Status) : base(Status)
        {
        }
    }
}
