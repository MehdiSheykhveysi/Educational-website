using System;

namespace Site.Core.Infrastructures.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static bool HasValue(this string value,bool ignoreWhiteSpace = true)
        {
            return ignoreWhiteSpace ? !string.IsNullOrEmpty(value) : !string.IsNullOrWhiteSpace(value);
        }
        public static int ToInt(this string value)
        {
            return Convert.ToInt32(value);
        }
    }
}
