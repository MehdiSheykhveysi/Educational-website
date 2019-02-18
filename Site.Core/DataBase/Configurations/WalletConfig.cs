using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Configurations
{
    public class WalletConfig : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(p => p.Id).HasDefaultValueSql("newsequentialid()"); //Use Sequential Guid In SqlServer For Generate Key
            builder.Property(w => w.Description).HasMaxLength(300);
            builder.Property(w => w.TransactDate).HasColumnType("datetime");
        }
    }
}
