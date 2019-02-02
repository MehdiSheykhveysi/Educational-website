using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Configurations
{
    public class CustomUserConfig : IEntityTypeConfiguration<CustomUser>
    {
        public CustomUserConfig()
        {
        }

        public void Configure(EntityTypeBuilder<CustomUser> builder)
        {
            builder.Property(p=>p.Avatar).HasMaxLength(300);
            builder.Property(p=>p.PhoneNumber).HasMaxLength(11);
            builder.Property(p => p.Wallet).HasColumnType("decimal(10, 2)");
        }
    }
}
