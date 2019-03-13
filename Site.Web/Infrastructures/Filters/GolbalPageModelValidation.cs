using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Site.Web.Infrastructures.Filters
{
    public class GolbalPageModelValidation : IAsyncPageFilter
    {
        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                PageModel page = context.HandlerInstance as PageModel;
                context.Result = (IActionResult)page?.Page()
                   ?? new BadRequestResult();
            }
            else
                await next.Invoke();
        }

        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            await Task.CompletedTask;
        }
    }
}
