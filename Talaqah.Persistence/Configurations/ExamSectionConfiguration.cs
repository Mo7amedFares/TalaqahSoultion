using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public class ExamSectionConfiguration : IEntityTypeConfiguration<ExamSection>
{
    public void Configure(EntityTypeBuilder<ExamSection> builder)
    {
        builder.ToTable("ExamSections");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.SkillType)
             .HasConversion<string>()
             .HasMaxLength(20)
             .IsRequired();

        builder.Property(x => x.DisplayOrder)
            .IsRequired();

        builder.HasOne(x => x.Exam)
            .WithMany(x => x.ExamSections)
            .HasForeignKey(x => x.ExamId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasMany(x => x.ExamQuestionRules)
            .WithOne(x => x.ExamSection)
            .HasForeignKey(x => x.ExamSectionId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}