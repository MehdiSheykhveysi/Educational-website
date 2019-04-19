using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Site.Core.DataBase.Repositories.CustomizeIdentity;
using Site.Core.Domain.Entities;
using Site.Web.Models.PagesModels;
using System.Threading.Tasks;

namespace Site.Web.Pages.Admin.UserManagment
{
    public class DeleteModel : PageModel
    {

        public DeleteModel(CustomUserManager customUserManager, IMapper mapper)
        {
            this.CustomUserManager = customUserManager;
            this.Mapper = mapper;
            this.Model = new AdminDeleteModel();
        }

        private readonly IMapper Mapper;
        private readonly CustomUserManager CustomUserManager;

        [BindProperty]
        public AdminDeleteModel Model { get; set; }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                ModelState.AddModelError("", "شناسه نامعتبر است");
                return Page();
            }
            CustomUser user = await CustomUserManager.FindByIdAsync(Id);
            Model = Mapper.Map(user, Model);
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            CustomUser user = await CustomUserManager.FindByIdAsync(Model.Id.ToString());
            user.IsDeleted = true;
            IdentityResult result = await CustomUserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                ViewData["Massege"] = "کاربر با موفقیت حذف شد";
                return RedirectToPage("/Admin/UserManagment/Index");
            }
            else
            {
                ViewData["Error"] = "مشکلی پیش آمده است لطفا دوباره امتحان کنید یا اطلاعات را دوباره چک کنید";
                return Page();
            }
        }
    }
}