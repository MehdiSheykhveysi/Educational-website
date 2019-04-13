using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Configurations
{
    public class KeywordkeyConfig : IEntityTypeConfiguration<Keywordkey>
    {
        public void Configure(EntityTypeBuilder<Keywordkey> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Title).HasMaxLength(50);

            //Relations
            builder.HasOne(c => c.ParentKeywordkey).WithMany(c => c.Keywordkeys).HasForeignKey(c => c.ParentKeywordkeyId);
        }
    }
}
