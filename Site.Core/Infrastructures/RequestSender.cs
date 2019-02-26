using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.Infrastructures
{
    public static class RequestSender
    {
        public static async Task<string> RequestAsync(HttpMethod pMethod, string pUrl, Dictionary<string, string> Pairs, CancellationToken cancellationToken)
        {
            string resultContent = "";
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(Pairs);
                var result = await client.PostAsync(pUrl, content, cancellationToken);
                resultContent = await result.Content.ReadAsStringAsync();
            }
            return resultContent;
        }

    }
}