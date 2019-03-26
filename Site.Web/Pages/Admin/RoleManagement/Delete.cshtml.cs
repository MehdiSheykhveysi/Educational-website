using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.Domain.Entities;
using Site.Web.Models.PagesModels.RoleManageModel;

namespace Site.Web.Pages.Admin.RoleManagement
{
    public class DeleteModel : PageModel
    {
        public DeleteModel(RoleManager<Role> RoleManager, IMapper mapper)
        {
            this.roleManager = RoleManager;
            this.Mapper = mapper;
        }

        private readonly RoleManager<Role> roleManager;
        private readonly IMapper Mapper;

        [BindProperty]
        public RoleDeleteModel Model { get; set; } = new RoleDeleteModel();

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return BadRequest();
            Role selectedRole = await roleManager.FindByIdAsync(Id);
            if (selectedRole == null)
                return NotFound();

            Model = Mapper.Map<RoleDeleteModel>(selectedRole);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Role markedRole = await roleManager.FindByIdAsync(Model.Id);
            if (markedRole == null)
                return BadRequest();
            IdentityResult result = await roleManager.DeleteAsync(markedRole);
            if (result.Succeeded)
            {
                ViewData["Massege"] = "نقش با موفقیت حذف شد";
                return RedirectToPage("/Admin/RoleManagement/index");
            }
            else
            {
                ViewData["Error"] = "مشکلی به وجود امد بعدا امتحان کنید";
            }
            return Page();
        }
    }
}