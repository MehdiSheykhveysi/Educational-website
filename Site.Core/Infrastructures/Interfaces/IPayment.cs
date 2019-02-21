using System;
using System.Collections.Generic;
using System.Text;

namespace Site.Core.Infrastructures.Interfaces
{
    public interface IPayment
    {
        PaymentRequest Pay(PayInput input);
        VerifyResponse Verify(string Authority);
    }
}
