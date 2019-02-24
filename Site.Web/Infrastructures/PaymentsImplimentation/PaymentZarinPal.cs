using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures.BusinessObjects;
using Site.Web.Infrastructures.Interfaces;
using System;
using System.Threading.Tasks;

namespace Site.Web.Infrastructures.PaymentImplimentation
{
    public class PaymentZarinPal// : IPayment
    {
        //private ZarinpalSandbox.Payment _payment { get; set; }
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private ISession _session => _httpContextAccessor.HttpContext.Session;

        //public PaymentZarinPal(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}

        //public async Task<PaymentRequest> PayAsync(PayInput input)
        //{
        //    _payment = new ZarinpalSandbox.Payment(Convert.ToInt32(input.Deposits));
        //    // var result = _payment.PaymentRequest(input.OrderId + input.Description, "https://localhost:5001/User/Wallet/Verify", input.Email, input.Mobile);
        //    var result = await _payment.PaymentRequest(input.Email + input.Description, "https://localhost:5001/User/Wallet/Verify");
        //    PaymentRequest pay = new PaymentRequest();
        //    if (result.Status==100)
        //    {
        //        _session.SetInt32(result.Authority, Convert.ToInt32(input.Deposits));

        //        pay.Authority = result.Authority;
        //        pay.Link = result.Link;
        //        pay.Status = result.Status;

        //    }
        //    return pay; ;
        //}

        //public async Task<VerifyResponse> VerifyAsync(string Authority)
        //{
        //    VerifyResponse verify = new VerifyResponse();
        //    if (!string.IsNullOrEmpty(Authority))
        //    {
        //        _payment = new ZarinpalSandbox.Payment(Convert.ToInt32(_session.GetInt32(Authority)));
        //        _session.Clear();
        //        _session.Remove(Authority);
        //        var result =await _payment.Verification(Authority);
        //        if (result.Status == 100)
        //        {
        //            verify.Status = result.Status;
        //            verify.RefId = result.RefId;
        //            verify.Authority = Authority;
        //        }
        //    }
        //    return verify;
        //}
    }
}