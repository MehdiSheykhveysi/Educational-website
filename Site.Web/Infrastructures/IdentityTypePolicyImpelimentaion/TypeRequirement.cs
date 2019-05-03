using Microsoft.AspNetCore.Authorization;
using Site.Core.Infrastructures.Utilities.Enums;

namespace Site.Web.Infrastructures.IdentityTypePolicyImpelimentaion
{
    public class TypeRequirement : IAuthorizationRequirement
    {
        public TypeRequirement(params CustomClaimType[] customClaims)
        {
            CustomClaims = customClaims;
        }

        public CustomClaimType[] CustomClaims { get; }
    }
}
