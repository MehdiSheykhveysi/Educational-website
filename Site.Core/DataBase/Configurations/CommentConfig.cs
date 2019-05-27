using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Configurations
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Body).HasMaxLength(250);
            builder.Property(c => c.Name).HasMaxLength(50);

            //Relations

            builder.HasOne(c => c.Course).WithMany(c => c.Comments).HasForeignKey(c => c.CoourseId);
            builder.HasOne(c => c.User).WithMany(c => c.Comments).HasForeignKey(c => c.UserId);
        }
    }
}
