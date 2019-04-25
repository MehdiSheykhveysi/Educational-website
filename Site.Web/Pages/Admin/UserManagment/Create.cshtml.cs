using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Repositories.CustomizeIdentity;
using Site.Core.Domain.Entities;
using Site.Web.Infrastructures.Interfaces;
using Site.Web.Models.PagesModels;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Pages.Admin.UserManagment
{
    public class CreateModel : PageModel
    {
        public CreateModel(CustomUserManager userManager, RoleManager<Role> roleManager, IHostingEnvironment hostingEnvironment, IFileHandler imageHandler, IMapper mapper)
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;
            this.HostingEnvironment = hostingEnvironment;
            this.ImageHandler = imageHandler;
            this.Mapper = mapper;
        }

        private readonly IMapper Mapper;
        private readonly IFileHandler ImageHandler;
        private readonly IHostingEnvironment HostingEnvironment;
        private readonly CustomUserManager UserManager;
        private readonly RoleManager<Role> RoleManager;

        [BindProperty]
        public AdminCreateModel Model { get; set; } = new AdminCreateModel();

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            Mapper.Map(await RoleManager.Roles.AsNoTracking().ToListAsync(cancellationToken), Model.SelectedRoles);
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            CustomUser user = new CustomUser(DateTime.Now);
            user = Mapper.Map(Model, user);
            string uploads = Path.Combine(HostingEnvironment.WebRootPath, "images", "UserProfile");
            user.Avatar = await ImageHandler.UploadFileAsync(Model.FormFile, uploads, cancellationToken);
            IdentityResult result = await UserManager.CreateAsync(user, Model.PassWord);
            IdentityResult identityResult = await UserManager.AddToRolesAsync(user, Model.SelectedRoles.Where(r => r.Checked).Select(c => c.Name));
            if (result.Succeeded)
            {
                return RedirectToPage("/Admin/UserManagment/index");
            }
            else
            {
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.TryAddModelError(item.Code, item.Description);
                }
            }
            return Page();
        }
    }
}