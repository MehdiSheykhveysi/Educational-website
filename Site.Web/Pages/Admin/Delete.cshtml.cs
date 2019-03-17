using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories.CustomizeIdentity;
using Site.Core.Domain.Entities;
using Site.Web.Models.PagesModels;
using System.Threading.Tasks;

namespace Site.Web.Pages.Admin
{
    public class DeleteModel : PageModel
    {

        public DeleteModel(CustomUserManager customUserManager, IMapper mapper)
        {
            this.CustomUserManager = customUserManager;
            this.Mapper = mapper;
        }

        private readonly IMapper Mapper;
        private readonly CustomUserManager CustomUserManager;

        [BindProperty]
        public AdminDeleteModel Model { get; set; } = new AdminDeleteModel();

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                ModelState.AddModelError("", "شناسه نامعتبر است");
                return Page();
            }
            CustomUser user = await CustomUserManager.FindByIdAsync(Id);
            Model = Mapper.Map<AdminDeleteModel>(user);
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            CustomUser user = await CustomUserManager.FindByIdAsync(Model.Id.ToString());
            IdentityResult result = await CustomUserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["Massege"] = "کاربر با موفقیت حذف شد";
                return RedirectToPage("/Admin/Index");
            }
            else
            {
                TempData["Error"] = "مشکلی پیش آمده است لطفا دوباره امتحان کنید یا اطلاعات را دوباره چک کنید";
                return Page();
            }
        }
    }
}