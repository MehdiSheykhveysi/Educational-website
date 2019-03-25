using Newtonsoft.Json;
using Site.Core.Infrastructures.Utilities.Enums;
using System;
using System.Linq;

namespace Site.Core.Infrastructures.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static bool HasValue(this string value, bool ignoreWhiteSpace = true)
        {
            return ignoreWhiteSpace ? !string.IsNullOrEmpty(value) : !string.IsNullOrWhiteSpace(value);
        }
        public static int ToInt(this string value)
        {
            return Convert.ToInt32(value);
        }
        public static T ToCsharpObject<T>(this string JsonString) where T : class
        {
            return JsonConvert.DeserializeObject<T>(JsonString);
        }
        public static decimal ToDecimal(this string value)
        {
            return Convert.ToDecimal(value);
        }
        public static string ToPersionDisplayEnum(this string value)
        {
            CustomClaimTypes[] list = (CustomClaimTypes[])Enum.GetValues(typeof(CustomClaimTypes));
            return list.SingleOrDefault(c => c.ToString().Equals(value, StringComparison.CurrentCultureIgnoreCase)).ToDisplay();
        }
    }
}
