using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Context
{
    public class LearningSiteDbContext:IdentityDbContext<CustomUser>
    {
        public LearningSiteDbContext(DbContextOptions<LearningSiteDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder) 
        {
             base.OnModelCreating(builder);
             builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        } 
    }
}
