using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Configurations
{
    class CourseEpisodConfig : IEntityTypeConfiguration<CourseEpisod>
    {
        public void Configure(EntityTypeBuilder<CourseEpisod> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Title).HasMaxLength(60);

            //Relations
            builder.HasOne(c => c.Course).WithMany(c => c.CourseEpisods).HasForeignKey(c => c.CourseId);
        }
    }
}
