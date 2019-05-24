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
            builder.Property(p => p.Id).HasDefaultValueSql("newsequentialid()"); //Use Sequential Guid In SqlServer For Generate Key
            builder.Property(p => p.UserName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(50).IsRequired();
            builder.Property(p => p.ShowUserName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.PasswordHash).HasMaxLength(100).IsRequired();
            builder.Property(p => p.EmailConfirmed).IsRequired();
            builder.Property(p => p.Avatar).HasMaxLength(300);
            builder.Property(p => p.PhoneNumber).HasMaxLength(11);
            builder.Property(p => p.PaymentToken).HasMaxLength(20);
            builder.Property(p => p.RegisterDate).HasColumnType("datetime");

            //Relations
            builder.HasMany(c => c.UserRoles).WithOne(c => c.User).HasForeignKey(c => c.UserId);
            builder.HasMany(u => u.Transactions).WithOne(t => t.CustomUser).HasForeignKey(u => u.CustomUserId);

        }
    }
}
