namespace Site.Core.Infrastructures.Utilities.Extensions
{
    public static class BoolExtension
    {
        public static string ToMessageString(this bool? v, string trueString, string falseString, string nullString = "Undefined")
        {
            return v == null ? nullString : v.Value ? trueString : falseString;
        }
        public static string ToMessageString(this bool v, string trueString, string falseString)
        {
            return ToMessageString(v, trueString, falseString, null);
        }
    }
}
