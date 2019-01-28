using Microsoft.AspNetCore.Identity;
using Site.Core.Domain.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Site.Web.Infrastructures
{
    public class GetUser
    {
        private readonly UserManager<CustomUser> _userManager;
        public GetUser(UserManager<CustomUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CustomUser> GetloggedUser(ClaimsPrincipal user)
        {
            CustomUser loggedUser = await _userManager.GetUserAsync(user);
            return loggedUser;
        }
    }
}
