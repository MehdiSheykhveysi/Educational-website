using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.Utilities;
using Site.Web.Infrastructures.Interfaces;
using Site.Web.Models.PagesModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Pages.Admin
{
    public class EditModel : PageModel
    {
        public EditModel(UserManager<CustomUser> UserManager, RoleManager<Role> RoleManager, IHostingEnvironment HostingEnvironment, IImageHandler ImageHandler, IMapper Mapper)
        {
            userManager = UserManager;
            roleManager = RoleManager;
            hostingEnvironment = HostingEnvironment;
            imageHandler = ImageHandler;
            mapper = Mapper;
        }


        private readonly IMapper mapper;
        private readonly UserManager<CustomUser> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IImageHandler imageHandler;

        [BindProperty]
        public AdminEditModel Model { get; set; }

        public async Task OnGetAsync(string Id)
        {
            Model = new AdminEditModel();
            CustomUser user = await userManager.FindByIdAsync(Id);
            Model = mapper.Map(user, Model);
            List<string> Roles = userManager.GetRolesAsync(user).GetAwaiter().GetResult().ToList();
            //var RolesModel = Roles.Select(c => new RoleModel { Checked = true, Name = c });
            List<string> AllRole = await roleManager.Roles.Select(c => c.Name).AsNoTracking().ToListAsync();
            AllRole.Union(Roles);
            Model.SelectedRoles = AllRole.Select(c => new RoleModel { Name = c, Checked = false }).ToList();
            //AllRole.AddRange(Roles.Select(c => new Role { Name = c }));
            //var d = AllRole.Select(c => new RoleModel { Name = c.Name }).Distinct();
            //Model.SelectedRoles.AddRange(d);
            //List<RoleModel> RolesWithoutDuplicate = new List<RoleModel>();
            //RolesWithoutDuplicate.AddRange(AllRole.Where(rs => Roles.All(r => r != rs.Name)).Select(c => new RoleModel { Name = c.Name, Checked = false }));
            Model.SelectedRoles.ForEach(c => Roles.ForEach(u =>
            {
                if (u == c.Name)
                {
                    c.Checked = true;
                }
            }));

            //foreach (var SystemRole in Model.SelectedRoles)
            //{
            //    foreach (var UserRole in Roles)
            //    {
            //        if (SystemRole.Name == UserRole)
            //        {
            //            SystemRole.Checked = true;
            //        }
            //    }
            //}
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
                CustomUser user = await userManager.FindByIdAsync(Model.Id);
                user = mapper.Map(Model, user);

                if (Assert.NotNull(Model.FormFile))
                {
                    string uploads = Path.Combine(hostingEnvironment.WebRootPath, "images", "UserProfile");
                    string OldProfileImagePath = $"{hostingEnvironment.WebRootPath}\\images\\UserProfile\\{Model.Avatar}";
                    user.Avatar = await imageHandler.UploadImageAsync(Model.FormFile, uploads, cancellationToken, OldProfileImagePath);
                }

                IEnumerable<string> SelectedRoles = Model.SelectedRoles.Where(r => r.Checked).Select(c => c.Name);
                IList<string> UserRoles = await userManager.GetRolesAsync(user);

                if (!UserRoles.SequenceEqual(SelectedRoles))
                {
                    await userManager.RemoveFromRolesAsync(user, UserRoles);
                    await userManager.AddToRolesAsync(user, SelectedRoles);
                }

                IdentityResult result = await userManager.UpdateAsync(user);
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
            return Page();
        }
    }
}