using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.Domain.Entities;
using Site.Web.Models.PagesModels.RoleManageModel;
using System.Collections.Generic;
using System.Linq;
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
            this.Model = new RoleDetailModel();
        }

        private readonly RoleManager<Role> RoleManager;
        private readonly IMapper Mapper;

        [BindProperty]
        public RoleDetailModel Model { get; set; }

        public async Task<IActionResult> OnGet(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return BadRequest();

            Role selectedRole = await RoleManager.FindByIdAsync(Id);

            if (selectedRole == null)
                return NotFound();

            Model = Mapper.Map(selectedRole, Model);
            IList<Claim> claims = await RoleManager.GetClaimsAsync(selectedRole);
            Model.Claims = claims.Select(c => new ClaimDTO { Value = c.Value, Checked = true }).ToList();

            return Page();
        }
    }
}