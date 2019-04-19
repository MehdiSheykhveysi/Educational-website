using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.Utilities.Enums;
using Site.Web.Infrastructures.Compares;
using Site.Web.Models.PagesModels.RoleManageModel;

namespace Site.Web.Pages.Admin.RoleManagement
{
    public class EditModel : PageModel
    {
        public EditModel(SignInManager<CustomUser> SignInManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            this.signInManager = SignInManager;
            this.RoleManager = roleManager;
            this.Mapper = mapper;
        }


        private readonly SignInManager<CustomUser> signInManager;
        private readonly RoleManager<Role> RoleManager;
        private readonly IMapper Mapper;

        [BindProperty]
        public RoleEditModel Model { get; set; } = new RoleEditModel();

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return BadRequest();

            Role role = await RoleManager.FindByIdAsync(Id);

            if (role == null)
                return NotFound();

            Model = Mapper.Map(role, Model);
            List<ClaimDTO> Roleclaims = RoleManager.GetClaimsAsync(role).GetAwaiter().GetResult().Select(c => new ClaimDTO { Value = c.Value, Checked = true }).ToList();
            List<ClaimDTO> AllClaims = Enum.GetNames(typeof(CustomClaimTypes)).Select(c => new ClaimDTO { Value = c, Checked = false }).ToList();
            //AllClaims.Union(Roleclaims);
            AllClaims.ForEach(c =>
            {
                if (Roleclaims.Contains(c, new ClaimDtoCompare()))
                    c.Checked = true;
            });

            Model.Claims = AllClaims;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Role role = await RoleManager.FindByIdAsync(Model.Id);
            role.Name = Model.Name;
            role.IsDeleted = Model.IsDeleted;
            string tempRoleName = role.Name;
            IdentityResult result = await RoleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                List<Claim> roleClaims = RoleManager.GetClaimsAsync(role).GetAwaiter().GetResult().ToList();
                List<Claim> selectedRole = Model.Claims.Where(c => c.Checked).Select(c => new Claim(typeof(CustomClaimTypes).ToString(), c.Value)).ToList();

                if (!roleClaims.SequenceEqual(selectedRole, new ClaimCompare()))
                {

                    foreach (Claim claim in roleClaims)
                    {
                        await RoleManager.RemoveClaimAsync(role, claim);
                    }
                    foreach (var claim in selectedRole)
                    {
                        await RoleManager.AddClaimAsync(role, claim);
                    }
                }
                if (User.IsInRole(tempRoleName))
                {
                    CustomUser currentUser = await signInManager.UserManager.GetUserAsync(User);
                    await signInManager.RefreshSignInAsync(currentUser);
                }
                return RedirectToPage("/Admin/RoleManagement/index");
            }
            else
                ModelState.AddModelError("1", "مشکلی پیش امد بعدا امتحان کنید");
            return Page();
        }
    }
}