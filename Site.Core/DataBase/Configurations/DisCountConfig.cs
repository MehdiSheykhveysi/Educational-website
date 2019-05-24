using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Configurations
{
    class DisCountConfig : IEntityTypeConfiguration<DisCount>
    {
        public void Configure(EntityTypeBuilder<DisCount> builder)
        {
            builder.HasKey(d=>d.Id);
            builder.Property(d => d.Id).ValueGeneratedOnAdd();
            builder.Property(d => d.Title).HasMaxLength(20);
        }
    }
}
