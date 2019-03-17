using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories.CustomizeIdentity;
using Site.Web.Models.PagesModels;

namespace Site.Web.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public IndexModel(CustomUserManager userManager)
        {
            this.UserManager = userManager;
        }

        private readonly CustomUserManager UserManager;

        public AdminIndexModel Model { get; set; } = new AdminIndexModel();

        public async Task OnGetAsync(CancellationToken cancellationToken, int PageNumber = 1, bool IsDeleted = false, string UserName = null)
        {
            Model.List = await UserManager.GetPagedUserAsync(UserName, IsDeleted, 3, PageNumber, cancellationToken);
            Model.Username = UserName;
            Model.IsDeleted = IsDeleted;
            ViewData["SearchKey"] = UserName;
        }
    }
}