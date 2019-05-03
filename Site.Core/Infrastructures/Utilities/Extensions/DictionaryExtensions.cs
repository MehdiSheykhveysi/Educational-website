using System.Collections.Generic;
using System.Linq;

namespace Site.Core.Infrastructures.Utilities.Extensions
{
    public static class DictionaryExtensions
    {
        public static string ToQueryString<TKey, TValue>(this Dictionary<TKey, TValue> dictionary) =>
            string.Join("&", dictionary.Select(pair =>
            string.Format("{0}={1}", pair.Key.ToString(), pair.Value.ToString())));
    }
}
