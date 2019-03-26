using Site.Web.Models.PagesModels.RoleManageModel;
using System;
using System.Collections.Generic;

namespace Site.Web.Infrastructures.Compares
{
    public class ClaimDtoCompare : IEqualityComparer<ClaimDTO>
    {
        public bool Equals(ClaimDTO x, ClaimDTO y)
        {
            if (x.Value.Equals(y.Value, StringComparison.CurrentCultureIgnoreCase))
                return true;
            else
                return false;
        }

        public int GetHashCode(ClaimDTO obj)
        {
            return obj.ToString().ToLower().GetHashCode();
        }
    }
}
