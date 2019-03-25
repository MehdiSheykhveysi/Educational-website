using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Site.Core.Domain.Entities;
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
           // var u=UserManager.cla
            var user = (ClaimsIdentity)context.User.Identity;
            List<Claim> userClaims = user.Claims
                       //.Where(c => c.Type == ClaimTypes.Role);list.Any(i => i.ToString().Equals(c.Type)); CustomClaimTypes[] list = (CustomClaimTypes[])Enum.GetValues(typeof(CustomClaimTypes));
                       .ToList();
            
            if (!context.User.Claims.Any(c => c.Type.IndexOf(nameof(CustomClaimTypes), StringComparison.CurrentCultureIgnoreCase) > -1))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (requirement.CustomClaims.Any(c => userClaims.Any(i => i.Value.IndexOf(c.ToString(), StringComparison.CurrentCultureIgnoreCase) > -1)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
