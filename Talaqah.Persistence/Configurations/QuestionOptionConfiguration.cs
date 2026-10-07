using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
{
    public void Configure(EntityTypeBuilder<QuestionOption> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.OptionText).IsRequired();
        
        builder.HasIndex(e => new { e.QuestionId, e.IsCorrect })
               .IncludeProperties(e => e.Id)
               .HasDatabaseName("IX_QuestionOptions_Question_IsCorrect");
               
        builder.HasOne(d => d.Question)
               .WithMany(p => p.QuestionOptions)
               .HasForeignKey(d => d.QuestionId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
