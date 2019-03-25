using Microsoft.AspNetCore.Authorization;
using Site.Core.Infrastructures.Utilities.Enums;

namespace Site.Web.Infrastructures.IdentityTypePolicyImpelimentaion
{
    public class TypeRequirement : IAuthorizationRequirement
    {
        public TypeRequirement(params CustomClaimTypes[] customClaims)
        {
            CustomClaims = customClaims;
        }

        public CustomClaimTypes[] CustomClaims { get; }
    }
}
