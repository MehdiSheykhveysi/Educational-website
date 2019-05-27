using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.Utilities.Extensions;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Context
{
    public class LearningSiteDbContext : IdentityDbContext<CustomUser, Role, Guid, IdentityUserClaim<Guid>,
    UserRole, IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public LearningSiteDbContext(DbContextOptions options) : base(options)
        {

        }

        //Disable Client Site Evaluation
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.SetUpDecimal();
            builder.RegisterDbSetClass<IEntity>(this.GetType().Assembly);
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        public async Task OpenConnectionAsync(CancellationToken cancellationToken)
        {
            await Database.OpenConnectionAsync(cancellationToken);
        }

        public DbCommand Command()
        {
            DbCommand cmd = Database.GetDbConnection().CreateCommand();
            return cmd;
        }
    }

}
