using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Configurations
{
    class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.CourseTitle).HasMaxLength(50);
            builder.Property(c => c.CourseDescription).HasMaxLength(400);
            builder.Property(c => c.ImageName).HasMaxLength(255);
            builder.Property(c => c.DemoFileName).HasMaxLength(255);

            //Relations
            builder.HasMany(c => c.Keywordkeys).WithOne(c => c.Course).HasForeignKey(c => c.CourseId);
            builder.HasOne(c => c.CustomUser).WithMany(c => c.Courses).HasForeignKey(c => c.CustomUserId);
        }
    }
}
