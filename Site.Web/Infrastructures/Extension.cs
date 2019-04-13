using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Site.Web.Infrastructures
{
    public static class Extension
    {
        public static void AddModelStateError<TErrorType>(this ModelStateDictionary ModelState, TErrorType Errors) where TErrorType : IEnumerable<string>
        {
            foreach (var item in Errors)
            {
                ModelState.AddModelError("-", item);
            }
        }
    }
}
