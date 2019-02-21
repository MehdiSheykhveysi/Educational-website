using Site.Core.Infrastructures.Interfaces;
using System;

namespace Site.Core.Infrastructures.Implimentation
{
    public class Payment : IPayment
    {
        public PaymentRequest Pay(PayInput input)
        {
            throw new NotImplementedException();
        }

        public VerifyResponse Verify(string Authority)
        {
            throw new NotImplementedException();
        }
    }
}
