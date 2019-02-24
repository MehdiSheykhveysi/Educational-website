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
using Site.Web.Infrastructures;
using Site.Web.Infrastructures.ImplementationInterfaces;
using Site.Web.Infrastructures.Interfaces;
using Site.Core.ApplicationService.SiteSettings;
using Site.Web.Infrastructures.PaymentsImplimentation;

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
            }).AddEntityFrameworkStores<LearningSiteDbContext>().AddDefaultTokenProviders();
            services.AddDbContext<LearningSiteDbContext>(options => options.UseSqlServer(siteSetting.DefaultConnection));
            services.AddTransient<GetUser, GetUser>();
            services.AddMvc();
            services.AddAutoMapper();
            services.AddTransient<IEmailHandler, EmailHandler>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IImageHandler, ImageHandler>();
            services.AddTransient<IImageWriter, ImageWriter>();
            services.AddTransient<IWalletRepository, WalletRepository>();
            services.AddTransient<IPayment,PaymnetPayIr>();
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