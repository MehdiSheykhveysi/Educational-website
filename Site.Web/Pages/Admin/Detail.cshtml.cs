using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories;
using Site.Core.DataBase.Repositories.CustomizeIdentity;
using Site.Core.Domain.Entities;
using Site.Web.Models.PagesModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Pages.Admin
{
    public class DetailModel : PageModel
    {

        public DetailModel(CustomUserManager customUserManager, IMapper mapper, ITransactRepository transactRepository)
        {
            this.CustomUserManager = customUserManager;
            this.Mapper = mapper;
            this.TransactRepository = transactRepository;
        }

        private readonly CustomUserManager CustomUserManager;
        private readonly IMapper Mapper;
        private readonly ITransactRepository TransactRepository;

        public AdminDetailModel Model { get; set; } = new AdminDetailModel();

        public async Task<IActionResult> OnGetAsync(string Id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(Id))
            {
                TempData["Error"] = "شناسه نا معتبر";
                return Page();
            }
            CustomUser user = await CustomUserManager.FindByIdAsync(Id);
            Model = Mapper.Map<AdminDetailModel>(user);
            List<string> roles = CustomUserManager.GetRolesAsync(user).GetAwaiter().GetResult().ToList();
            Model.SelectedRoles = roles.Select(c => new RoleModel { Name = c, Checked = true }).ToList();
            List<Transact> transactList = await TransactRepository.GetWalletByUserIdAsync(user.Id, cancellationToken);
            Model.Transactions = Mapper.Map<List<TransactModel>>(transactList);
            return Page();
        }
    }
}