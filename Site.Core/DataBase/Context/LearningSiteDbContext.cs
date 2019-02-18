using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.Utilities.Extensions;

namespace Site.Core.DataBase.Context
{
    public class LearningSiteDbContext : IdentityDbContext<CustomUser, Role, int>
    {
        public LearningSiteDbContext(DbContextOptions options) : base(options)
        {

        }

        //public DbSet<Wallet> Wallets { get; set; }
        //public DbSet<CustomUser> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.SetUpDecimal();
            builder.RegisterDbSetClass<IEntity>(this.GetType().Assembly);
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }

}
