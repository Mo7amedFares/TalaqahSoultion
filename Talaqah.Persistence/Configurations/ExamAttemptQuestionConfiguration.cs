using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public class ExamAttemptQuestionConfiguration : IEntityTypeConfiguration<ExamAttemptQuestion>
{
    public void Configure(EntityTypeBuilder<ExamAttemptQuestion> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
