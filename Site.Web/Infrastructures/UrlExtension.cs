using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Site.Web.Infrastructures
{
    public static class UrlExtension
    {
        public static string PathAndQuery(this HttpRequest request) =>
        request.QueryString.HasValue
        ? $"{request.Path}{request.QueryString}"
        : request.Path.ToString();
        public static string ToQueryString(this object obj)
        {
            if (obj == null) return "";

            return "?" + string.Join("&", obj.GetType()
                                       .GetProperties()
                                       .Where(p => Attribute.IsDefined(p, typeof(DataMemberAttribute)) && p.GetValue(obj, null) != null)
                                       .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(obj).ToString())}"));
        }
    }
}
