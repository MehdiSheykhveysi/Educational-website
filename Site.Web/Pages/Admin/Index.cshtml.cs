using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories;
using Site.Web.Models.PagesModels;

namespace Site.Web.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public IndexModel(IUserRepository UserRepository)
        {
            userRepository = UserRepository;
        }

        public IUserRepository userRepository { get; set; }
        public AdminIndexModel Model { get; set; } = new AdminIndexModel();

        public async Task OnGetAsync(CancellationToken cancellationToken,int PageNumber = 1, string UserName = null)
        {
            Model.List = await userRepository.GetPagedUserAsync(UserName, 3, PageNumber, cancellationToken);
            Model.Username = UserName;
            ViewData["SearchKey"] = UserName;
        }
    }
}