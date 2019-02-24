using Microsoft.Extensions.Options;
using Site.Core.ApplicationService.SiteSettings;
using Site.Core.Infrastructures.Utilities.Extensions;
using Site.Web.Infrastructures.BusinessObjects;
using Site.Web.Infrastructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Site.Core.Infrastructures;
using System.Net.Http;
using System.Threading;

namespace Site.Web.Infrastructures.PaymentsImplimentation
{
    public class PaymnetPayIr : IPayment
    {
        private readonly SiteSetting siteSetting;
        public PaymnetPayIr(IOptionsSnapshot<SiteSetting> SiteSetting)
        {
            siteSetting = SiteSetting.Value;
        }

        public async Task<PaymentRequest> PayAsync(PayInput input, CancellationToken cancellationToken)
        {
            Dictionary<string, string> post_values = new Dictionary<string, string>
            {
                { "api", siteSetting.Api },
                { "amount", input.Deposits.ToString() },
                { "redirect", input.Redirect },
                { "description", input.Description },
            };
            string response = await RequestSender.RequestAsync(HttpMethod.Post, siteSetting.GatewaySend, post_values,cancellationToken);
            return response.ToCsharpObject<PaymentRequest>();
        }

        public Task<VerifyResponse> VerifyAsync(string Authority, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
