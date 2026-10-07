using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public sealed class ExamQuestionRuleConfiguration
    : IEntityTypeConfiguration<ExamQuestionRule>
{
    public void Configure(EntityTypeBuilder<ExamQuestionRule> builder)
    {
        
        builder.ToTable("ExamQuestionRules", table =>
        {
            table.HasCheckConstraint(
                "CK_ExamQuestionRule_NumberOfQuestions",
                "[NumberOfQuestions] > 0");
        });

        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedOnAdd();

        
        builder.Property(x => x.CefrLevel)
               .HasConversion<string>()
               .HasColumnType("char(2)")
               .IsRequired();

        builder.Property(x => x.NumberOfQuestions)
               .IsRequired();

        
        builder.HasOne(x => x.ExamSection)
               .WithMany(x => x.ExamQuestionRules)
               .HasForeignKey(x => x.ExamSectionId)
               .OnDelete(DeleteBehavior.Cascade);

        
        builder.HasIndex(x => new
        {
            x.ExamSectionId,
            x.CefrLevel
        })
        .IsUnique()
        .HasDatabaseName("UX_ExamQuestionRule_ExamSection_CefrLevel");

        
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}