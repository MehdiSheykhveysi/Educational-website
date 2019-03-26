using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Site.Web.Infrastructures.Compares
{
    public class ClaimCompare : IEqualityComparer<Claim>
    {
        public bool Equals(Claim x, Claim y)
        {
            if (x.Value.Equals(y.Value, StringComparison.CurrentCulture))
                return true;
            else
                return false;
        }

        public int GetHashCode(Claim obj)
        {
            return obj.ToString().ToLower().GetHashCode();
        }
    }
}
