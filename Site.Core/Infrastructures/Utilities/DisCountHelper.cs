using Site.Core.Domain.Entities;
using System;

namespace Site.Core.Infrastructures.Utilities
{
    public static class DisCountHelper
    {
        public static decimal ComputeDisCount(DisCount disCount, decimal Amount)
        {
            if (disCount.Count <= 0 || disCount.StartDate != null && disCount.StartDate > DateTime.Now || disCount.MaxDate != null && disCount.MaxDate < DateTime.Now)
                return Amount;
            return (Amount) - (Amount * disCount.DisCountPercent / 100);
        }
        public static decimal WithdrawFromWallet(CustomUser user, decimal Amount)
        {
            decimal remainUserBalance = Amount - user.AccountBalance;


            if (remainUserBalance < 0)
            {
                remainUserBalance = 0;
                user.AccountBalance -= Amount;
            }
            return remainUserBalance;
        }
    }
}
