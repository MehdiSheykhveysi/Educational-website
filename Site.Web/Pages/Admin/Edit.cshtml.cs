﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Repositories.CustomizeIdentity;
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
        public EditModel(CustomUserManager userManager, RoleManager<Role> roleManager, IHostingEnvironment hostingEnvironment, IImageHandler imageHandler, IMapper mapper)
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;
            this.HostingEnvironment = hostingEnvironment;
            this.ImageHandler = imageHandler;
            this.Mapper = mapper;
        }


        private readonly IMapper Mapper;
        private readonly CustomUserManager UserManager;
        private readonly RoleManager<Role> RoleManager;
        private readonly IHostingEnvironment HostingEnvironment;
        private readonly IImageHandler ImageHandler;

        [BindProperty]
        public AdminEditModel Model { get; set; }

        public async Task OnGetAsync(string Id)
        {
            if (string.IsNullOrEmpty(Id)) ModelState.AddModelError("", "شناسه وارده نام عتبر است");
            
            Model = new AdminEditModel();
            CustomUser user = await UserManager.FindByIdAsync(Id);
            Model = Mapper.Map(user, Model);
            List<string> Roles = UserManager.GetRolesAsync(user).GetAwaiter().GetResult().ToList();
            //var RolesModel = Roles.Select(c => new RoleModel { Checked = true, Name = c });
            List<string> AllRole = await RoleManager.Roles.Select(c => c.Name).AsNoTracking().ToListAsync();
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
            CustomUser user = await UserManager.FindByIdAsync(Model.Id);
            user = Mapper.Map(Model, user);

            if (Assert.NotNull(Model.FormFile))
            {
                string uploads = Path.Combine(HostingEnvironment.WebRootPath, "images", "UserProfile");
                string OldProfileImagePath = $"{HostingEnvironment.WebRootPath}\\images\\UserProfile\\{Model.Avatar}";
                user.Avatar = await ImageHandler.UploadImageAsync(Model.FormFile, uploads, cancellationToken, OldProfileImagePath);
            }

            IEnumerable<string> SelectedRoles = Model.SelectedRoles.Where(r => r.Checked).Select(c => c.Name);
            IList<string> UserRoles = await UserManager.GetRolesAsync(user);

            if (!UserRoles.SequenceEqual(SelectedRoles))
            {
                await UserManager.RemoveFromRolesAsync(user, UserRoles);
                await UserManager.AddToRolesAsync(user, SelectedRoles);
            }

            IdentityResult result = await UserManager.UpdateAsync(user);
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