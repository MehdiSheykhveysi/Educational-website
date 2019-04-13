using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Site.Core.Infrastructures.Utilities.Extensions;
using Site.Web.Infrastructures.BusinessObjects;

namespace Site.Web.Infrastructures.Filters
{
    public class GlobalMvcValidateModelStateAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.IsAjaxRequest() || context.ModelState.IsValid)
            {
                if (context.HttpContext.Request.IsAjaxRequest() && !context.ModelState.IsValid)
                {
                    AjaxResult result = new AjaxResult()
                    {
                        Status = "Error"
                    };
                    result.Errors.AddRange(context.ModelState.Values.SelectMany(c => c.Errors).Select(c => c.ErrorMessage).ToList());
                    context.Result = new JsonResult(result);
                }
            }
            else
            {
                Controller controller = context.Controller as Controller;
                object model = context.ActionArguments.Any()
                   ? context.ActionArguments.First().Value
                   : null;

                context.Result = (IActionResult)controller?.View(model)
                   ?? new BadRequestResult();
            }
            return base.OnActionExecutionAsync(context, next);
        }
    }
}