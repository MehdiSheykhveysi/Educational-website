using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;

namespace Site.Web
{
    public class Startup 
    {
        public Startup (IConfiguration configuration) 
        {
            Configuration = configuration;
        }
        private IConfiguration Configuration { get; set; }
        public void ConfigureServices (IServiceCollection services) 
        {
            services.AddIdentity<CustomUser,IdentityRole>(Options=>{
                Options.Password.RequireNonAlphanumeric=false;
                Options.Password.RequireLowercase=false;
                Options.Password.RequiredUniqueChars=2;
                Options.Password.RequireDigit=false;
            }).AddEntityFrameworkStores<LearningSiteDbContext>().AddDefaultTokenProviders();
            services.AddDbContext<LearningSiteDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc ();
        }

        public void Configure (IApplicationBuilder app, IHostingEnvironment env) 
        {
            if (env.IsDevelopment ()) 
            {
                app.UseDeveloperExceptionPage ();
            }

            app.UseStaticFiles ();
            app.UseMvcWithDefaultRoute ();

        }
    }
}