using Site.Web.Infrastructures.BusinessObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Infrastructures.Interfaces
{
    public interface IPayment
    {
        Task<PaymentRequest> PayAsync(PayInput input, CancellationToken cancellationToken);
        Task<VerifyResponse> VerifyAsync(string Authority, CancellationToken cancellationToken);
    }
}
