using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.Utilities.Extensions;
using System;

namespace Site.Core.DataBase.Context
{
    public class LearningSiteDbContext : IdentityDbContext<CustomUser, Role, Guid, IdentityUserClaim<Guid>,
    UserRole, IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public LearningSiteDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.SetUpDecimal();
            builder.RegisterDbSetClass<IEntity>(this.GetType().Assembly);
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }

}
