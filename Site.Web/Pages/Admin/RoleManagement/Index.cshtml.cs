using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Site.Core.Domain.Entities;
using Site.Web.Models.PagesModels.RoleManageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Pages.Admin.RoleManagement
{
    public class IndexModel : PageModel
    {
        public IndexModel(RoleManager<Role> roleManager, IMapper mapper)
        {
            RoleManager = roleManager;
            Mapper = mapper;
        }
        private readonly RoleManager<Role> RoleManager;
        private readonly IMapper Mapper;

        public RoleIndexModel Model { get; set; } = new RoleIndexModel();


        public async Task OnGetAsync(CancellationToken cancellationToken, bool IsDeleted = false, string RoleName = null)
        {
            @ViewData["SearchKey"] = RoleName;
            List<Role> Roles =
               (IsDeleted || !string.IsNullOrEmpty(RoleName)) ?

                (await RoleManager.Roles.Where(c => c.Name.Contains(RoleName, StringComparison.CurrentCultureIgnoreCase) && c.IsDeleted == IsDeleted).AsNoTracking().ToListAsync(cancellationToken)) :

                (await RoleManager.Roles.Where(c => string.IsNullOrEmpty(RoleName) || c.Name.IndexOf(RoleName, StringComparison.CurrentCultureIgnoreCase) != -1).AsNoTracking().ToListAsync(cancellationToken));

            Model.Roles = Mapper.Map<List<RoleManageModel>>(Roles);
            Model.IsDeleted = IsDeleted;
        }
    }
}