using Microsoft.EntityFrameworkCore;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(o => o.AnonymousUserId).HasMaxLength(14);

            //Relations
            builder.HasOne(o => o.Client).WithMany(c => c.Orders).HasForeignKey(o => o.ClientId);
            builder.HasMany(o => o.OrderDetails).WithOne(od => od.Order).HasForeignKey(o => o.OrderId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
