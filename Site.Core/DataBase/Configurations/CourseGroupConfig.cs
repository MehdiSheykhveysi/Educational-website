using Microsoft.EntityFrameworkCore;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Configurations
{
    class CourseGroupConfig : IEntityTypeConfiguration<CourseGroup>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CourseGroup> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Title).HasMaxLength(60);

            //Relations
            builder.HasOne(c => c.ParentCourseGroup).WithMany(c => c.Groups).HasForeignKey(c => c.ParentId);
            builder.HasMany(c => c.Courses).WithOne(c => c.CourseGroup).HasForeignKey(c => c.CourseGroupId);
        }
    }
}
