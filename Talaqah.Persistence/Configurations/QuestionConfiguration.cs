using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.SkillType)
                .HasConversion<string>()
                .HasMaxLength(20);

        builder.Property(e => e.CefrLevel)
                .HasConversion<string>()
                .HasColumnType("varchar(2)")
                .HasMaxLength(2);
        
        builder.Property(e => e.QuestionType)
                .HasConversion<string>()
                .HasMaxLength(15);
        
        builder.Property(e => e.QuestionText).IsRequired();
        
        builder.Property(e => e.MediaUrl).HasMaxLength(2083);

        // Scale Index Optimization for pulling random parameters rapidly across 150k rows
        builder.HasIndex(e => new { e.SkillType, e.CefrLevel, e.QuestionType })
            .HasDatabaseName("IX_Questions_Selection");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
