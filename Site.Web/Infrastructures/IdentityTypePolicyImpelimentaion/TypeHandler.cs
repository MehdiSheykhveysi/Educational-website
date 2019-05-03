using Microsoft.AspNetCore.Authorization;
using Site.Core.Infrastructures.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Site.Web.Infrastructures.IdentityTypePolicyImpelimentaion
{
    public class TypeHandler : AuthorizationHandler<TypeRequirement>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TypeRequirement requirement)
        {

            ClaimsIdentity user = (ClaimsIdentity)context.User.Identity;
            List<Claim> userClaims = user.Claims
                       //.Where(c => c.Type == ClaimTypes.Role);list.Any(i => i.ToString().Equals(c.Type)); CustomClaimTypes[] list = (CustomClaimTypes[])Enum.GetValues(typeof(CustomClaimTypes));
                       .ToList();

            if (!context.User.Claims.Any(c => c.Type.IndexOf(nameof(CustomClaimType), StringComparison.CurrentCultureIgnoreCase) > -1))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (requirement.CustomClaims.Any(c => userClaims.Any(i => i.Value.Equals(c.ToString(), StringComparison.CurrentCultureIgnoreCase) && i.Type.Equals(typeof(CustomClaimType).ToString(), StringComparison.CurrentCultureIgnoreCase))))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
