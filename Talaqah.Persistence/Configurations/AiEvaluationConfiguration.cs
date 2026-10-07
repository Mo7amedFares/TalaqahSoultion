using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public class AiEvaluationConfiguration : IEntityTypeConfiguration<AiEvaluation>
{
    public void Configure(EntityTypeBuilder<AiEvaluation> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.ErrorAnalysis).IsRequired();
        builder.Property(e => e.Recommendations).IsRequired();
        
        builder.HasOne(d => d.StudentResponse)
               .WithOne(p => p.AiEvaluation)
               .HasForeignKey<AiEvaluation>(d => d.StudentResponseId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CHK_AiEval_ErrorAnalysis_IsJson", "ISJSON([ErrorAnalysis]) = 1");
            t.HasCheckConstraint("CHK_AiEval_Recommendations_IsJson", "ISJSON([Recommendations]) = 1");
        });

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
