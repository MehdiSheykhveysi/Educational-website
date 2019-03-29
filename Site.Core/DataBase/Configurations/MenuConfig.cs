using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Configurations
{
    class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Content).HasMaxLength(60);
            builder.Property(c => c.Url).HasMaxLength(300);

            //Relations
            builder.HasOne(c => c.ParentMenu).WithMany(c=>c.MenuItems).HasForeignKey(c=>c.ParentMenuId);
        }
    }
}
