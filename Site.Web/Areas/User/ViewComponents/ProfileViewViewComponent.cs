using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site.Core.Domain.Entities;
using Site.Web.Areas.User.Models.HomeModels;
using Site.Web.Infrastructures;
using System.Threading.Tasks;

namespace Site.Web.Areas.User.ViewComponents
{
    [ViewComponent]
    [Authorize]
    public class ProfileViewViewComponent : ViewComponent
    {
        private readonly GetUser _getUser;
        public ProfileViewViewComponent(GetUser getUser)
        {
            _getUser = getUser;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            CustomUser LoggedUser = await _getUser.GetloggedUser(HttpContext.User);
            UserProfileViewModel model = new UserProfileViewModel
            {
                UserName = LoggedUser.UserName,
                Email = LoggedUser.Email,
                PhoneNumber = LoggedUser.PhoneNumber,
                RegisterDate = LoggedUser.RegisterDate.ToShamsi(),
                Wallet = LoggedUser.Wallet,
                UserProfileUrl = LoggedUser.Avatar
            };
            return View("Default", model);
        }

    }
}
