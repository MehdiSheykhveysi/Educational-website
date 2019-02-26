using Microsoft.AspNetCore.Identity;
using Site.Core.Domain.Entities;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Site.Web.Infrastructures
{
    public class GetUser : IDisposable
    {
        private readonly UserManager<CustomUser> _userManager;
        public GetUser(UserManager<CustomUser> userManager)
        {
            _userManager = userManager;
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }

        public async Task<CustomUser> GetloggedUser(ClaimsPrincipal user)
        {
            CustomUser loggedUser = await _userManager.GetUserAsync(user);
            return loggedUser;
        }

        public async Task<TKey> GetloggedUserID<TKey>(ClaimsPrincipal user)
        {
            CustomUser loggedUser = await _userManager.GetUserAsync(user);
            object UserId = loggedUser.Id;
            return (TKey)UserId;
        }
    }
}