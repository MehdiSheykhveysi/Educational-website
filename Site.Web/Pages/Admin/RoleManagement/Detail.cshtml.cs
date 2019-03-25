using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.Domain.Entities;
using Site.Web.Models.PagesModels.RoleManageModel;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Site.Web.Pages.Admin.RoleManagement
{
    public class DetailModel : PageModel
    {
        public DetailModel(RoleManager<Role> roleManager, IMapper mapper)
        {
            this.RoleManager = roleManager;
            this.Mapper = mapper;
        }

        public RoleManager<Role> RoleManager{ get; set; }
        public IMapper Mapper { get; set; }

        public RoleDetailModel Model { get; set; } = new RoleDetailModel();

        public async Task OnGet(string Id)
        {
            Role selectedRole = await RoleManager.FindByIdAsync(Id);
            Model = Mapper.Map<RoleDetailModel>(selectedRole);
            IList<Claim> claims = await RoleManager.GetClaimsAsync(selectedRole);
            Model.Claims = Mapper.Map<List<ClaimDTO>>(claims);
        }
    }
}