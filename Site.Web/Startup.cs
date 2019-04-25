using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Site.Core.DataBase.Context;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures;
using Site.Core.Infrastructures.Implimentation;
using Site.Core.Infrastructures.Interfaces;
using Site.Web.Infrastructures.ImplementationInterfaces;
using Site.Web.Infrastructures.Interfaces;
using Site.Core.ApplicationService.SiteSettings;
using Site.Web.Infrastructures.PaymentsImplimentation;
using Site.Web.Infrastructures.Filters;
using Site.Core.DataBase.Repositories.CustomizeIdentity;
using Site.Core.Infrastructures.Utilities.Enums;
using Site.Web.Infrastructures.IdentityTypePolicyImpelimentaion;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;

namespace Site.Web
{
    public class Startup
    {
        private readonly SiteSetting siteSetting;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            siteSetting = configuration.GetSection(nameof(SiteSetting)).Get<SiteSetting>();
        }
        private IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSetting>(Configuration.GetSection("SiteSetting"));
            services.AddIdentity<CustomUser, Role>(Options =>
            {
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequireLowercase = false;
                Options.Password.RequireUppercase = false;
                Options.Password.RequiredUniqueChars = 2;
                Options.Password.RequireDigit = false;
                Options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<LearningSiteDbContext>().AddUserManager<CustomUserManager>().AddDefaultTokenProviders();
            services.AddDbContext<LearningSiteDbContext>(options => options.UseSqlServer(siteSetting.DefaultConnection));
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(GlobalMvcValidateModelStateAttribute));
                options.Filters.Add(typeof(GolbalPageModelValidation));
            });
            services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 6000000; });
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/LogIn";
            });

            services.ConfigureApplicationCookie(options =>
            {
                // add TimeSpan with 5 minutes plus timezone difference from Utc time
                options.Cookie.Expiration = DateTime.Now.Subtract(DateTime.UtcNow).Add(TimeSpan.FromMinutes(2));

            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(CustomClaimTypes.None), policy =>
                {
                    policy.AddRequirements(new TypeRequirement((CustomClaimTypes[])Enum.GetValues(typeof(CustomClaimTypes))));
                    //policy.RequireClaim(CustomClaimTypes.AdminSystem.ToString());
                });
                options.AddPolicy(nameof(CustomClaimTypes.AdminSystem), policy =>
                {
                    policy.AddRequirements(new TypeRequirement(CustomClaimTypes.AdminSystem));
                    //policy.RequireClaim(CustomClaimTypes.AdminSystem.ToString());
                });

                options.AddPolicy(nameof(CustomClaimTypes.UserManagment), policy =>
                {
                    policy.AddRequirements(new TypeRequirement(CustomClaimTypes.UserManagment, CustomClaimTypes.AdminSystem));
                    //policy.RequireClaim(CustomClaimTypes.UserManagment.ToString());
                });

                options.AddPolicy(nameof(CustomClaimTypes.RoleManagment), policy =>
                {
                    policy.AddRequirements(new TypeRequirement(CustomClaimTypes.RoleManagment, CustomClaimTypes.AdminSystem));
                    //policy.RequireClaim(CustomClaimTypes.RoleManagment.ToString());
                });

                options.AddPolicy(nameof(CustomClaimTypes.AddUser), policy =>
                {
                    policy.AddRequirements(new TypeRequirement(CustomClaimTypes.AddUser, CustomClaimTypes.AdminSystem, CustomClaimTypes.UserManagment));
                    //policy.RequireClaim(CustomClaimTypes.AddUser.ToString());
                });

                options.AddPolicy(nameof(CustomClaimTypes.EditUser), policy =>
                {
                    policy.AddRequirements(new TypeRequirement(CustomClaimTypes.EditUser, CustomClaimTypes.AdminSystem, CustomClaimTypes.UserManagment));
                    //policy.RequireClaim(CustomClaimTypes.EditUser.ToString());
                });

                options.AddPolicy(nameof(CustomClaimTypes.DeleteUser), policy =>
                {
                    policy.AddRequirements(new TypeRequirement(CustomClaimTypes.DeleteUser, CustomClaimTypes.AdminSystem, CustomClaimTypes.UserManagment));
                    //policy.RequireClaim(CustomClaimTypes.DeleteUser.ToString());
                });

                options.AddPolicy(nameof(CustomClaimTypes.AddRole), policy =>
                {
                    policy.AddRequirements(new TypeRequirement(CustomClaimTypes.AddRole, CustomClaimTypes.AdminSystem, CustomClaimTypes.RoleManagment));
                    //policy.RequireClaim(CustomClaimTypes.AddRole.ToString());
                });

                options.AddPolicy(nameof(CustomClaimTypes.EditRole), policy =>
                {
                    policy.AddRequirements(new TypeRequirement(CustomClaimTypes.EditRole, CustomClaimTypes.AdminSystem, CustomClaimTypes.RoleManagment));
                    //policy.RequireClaim(CustomClaimTypes.EditRole.ToString());
                });

                options.AddPolicy(nameof(CustomClaimTypes.DeleteRole), policy =>
                {
                    policy.AddRequirements(new TypeRequirement(CustomClaimTypes.DeleteRole, CustomClaimTypes.AdminSystem, CustomClaimTypes.RoleManagment));
                    //policy.RequireClaim(CustomClaimTypes.DeleteRole.ToString());
                });

            });

            services.AddSingleton<IAuthorizationHandler, TypeHandler>();

            services.AddAutoMapper(cfg => cfg.ValidateInlineMaps = false);

            services.AddScoped<CustomUserManager>();
            services.AddTransient<IEmailHandler, EmailHandler>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IFileHandler, FileHandler>();
            services.AddTransient<IFileWriter, FileWriter>();
            services.AddTransient<IFileHelper, FileHelper>();
            services.AddScoped<ITransactRepository, TransactRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseStatusRepositoty, CourseeStatusRepositoty>();
            services.AddScoped<ICourseLevelRepository, CourseLevelRepository>();
            services.AddScoped<ICourseGroupRepository, CourseGroupRepository>();
            services.AddScoped<ICourseEpisodRepository, CourseEpisodRepository>();
            services.AddScoped<IKeywordRepository, KeywordRepository>();
            services.AddScoped<IImageResizer, ImageResizer>();
            services.AddTransient<IPayment, PaymnetPayIr>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                 name: "default",
                 template: "{controller=Home}/{action=Index}/{id?}"
               );
            });

        }
    }
}