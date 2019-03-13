using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Site.Core.Domain.Entities;
using Site.Web.Infrastructures.Interfaces;
using Site.Web.Models.PagesModels;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Pages.Admin
{
    public class CreateModel : PageModel
    {
        public CreateModel(UserManager<CustomUser> UserManager, RoleManager<Role> RoleManager, IHostingEnvironment HostingEnvironment, IImageHandler ImageHandler, IMapper Mapper)
        {
            userManager = UserManager;
            roleManager = RoleManager;
            hostingEnvironment = HostingEnvironment;
            imageHandler = ImageHandler;
            mapper = Mapper;
        }

        private readonly IMapper mapper;
        private readonly IImageHandler imageHandler;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<CustomUser> userManager;
        private readonly RoleManager<Role> roleManager;

        [BindProperty]
        public AdminCreateModel Model { get; set; }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            Model = new AdminCreateModel();
            mapper.Map(await roleManager.Roles.AsNoTracking().ToListAsync(cancellationToken), Model.SelectedRoles);
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                CustomUser user = new CustomUser(DateTime.Now);
                user = mapper.Map(Model, user);
                string uploads = Path.Combine(hostingEnvironment.WebRootPath, "images", "UserProfile");
                user.Avatar = await imageHandler.UploadImageAsync(Model.FormFile, uploads, cancellationToken);
                IdentityResult result = await userManager.CreateAsync(user, Model.PassWord);
                IdentityResult identityResult = await userManager.AddToRolesAsync(user, Model.SelectedRoles.Where(r => r.Checked).Select(c => c.Name));
                if (result.Succeeded)
                {
                    return RedirectToPage("/Admin/index");
                }
                else
                {
                    foreach (IdentityError item in result.Errors)
                    {
                        ModelState.TryAddModelError(item.Code, item.Description);
                    }
                }
            }
            return Page();
        }
    }
}