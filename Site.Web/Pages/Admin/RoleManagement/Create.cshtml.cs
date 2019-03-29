using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.Utilities.Enums;
using Site.Web.Models.PagesModels.RoleManageModel;
using Site.Core.Infrastructures.Utilities.Extensions;
using System;

namespace Site.Web.Pages.Admin.RoleManagement
{

    [Authorize(Policy = nameof(CustomClaimTypes.AddRole))]
    public class CreateModel : PageModel
    {
        public CreateModel(RoleManager<Role> roleManager)
        {
            RoleManager = roleManager;
        }
        private readonly RoleManager<Role> RoleManager;

        [BindProperty]
        public RoleCreateModel Model { get; set; } = new RoleCreateModel();

        public void OnGet()
        {
            Array EnumValues = Enum.GetValues(typeof(CustomClaimTypes));
            Model.CustomClaims = new List<ClaimModel>();
            foreach (object item in EnumValues)
            {
                CustomClaimTypes CurrentEnum = (CustomClaimTypes)Enum.Parse(typeof(CustomClaimTypes), item.ToString());
                Model.CustomClaims.Add(new ClaimModel(CurrentEnum.ToDisplay(), CurrentEnum));
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            bool roleCheck = await RoleManager.RoleExistsAsync(Model.Name);
            if (!roleCheck)
            {
                ModelState.AddModelError("1", "نقش وارد شده تکراری است");

                return Page();
            }


            Role role = new Role()
            {
                Name = Model.Name,
            };
            var result = await RoleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                IEnumerable<ClaimModel> list = Model?.CustomClaims.Where(c => c.Checked);
                foreach (ClaimModel c in list)
                {
                    Claim claim = new Claim(typeof(CustomClaimTypes).ToString(), c.CustomClaim.ToString());

                    await RoleManager.AddClaimAsync(role, claim);
                }
                return RedirectToPage("/Admin/RoleManagement/Index");
            }
            else
            {
                ModelState.AddModelError("1", "خطایی رخ داده است بعدا امتحان کنید");
            }
            return Page();
        }
    }
}