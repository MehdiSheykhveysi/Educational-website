using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Configurations
{
    public class CustomUserConfig : IEntityTypeConfiguration<CustomUser>
    {
        
        public void Configure(EntityTypeBuilder<CustomUser> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p=>p.Id).HasDefaultValueSql("newsequentialid()"); //Use Sequential Guid In SqlServer For Generate Key
            builder.Property(p => p.Avatar).HasMaxLength(300);
            builder.Property(p => p.PhoneNumber).HasMaxLength(11);
            builder.Property(p=>p.RegisterDate).HasColumnType("datetime");

            //Relations

            builder.HasOne(u => u.Wallet).WithMany(w => w.CustomUsers).HasForeignKey(u => u.WalletID).IsRequired();

        }
    }
}
