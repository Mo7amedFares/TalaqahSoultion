using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public class StudentResponseConfiguration : IEntityTypeConfiguration<StudentResponse>
{
    public void Configure(EntityTypeBuilder<StudentResponse> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.AudioResponseUrl).HasMaxLength(2083);
        builder.Property(e => e.Score).HasPrecision(5, 2);

        builder.HasOne(sr => sr.ExamAttempt)
               .WithMany(ea => ea.StudentResponses)
               .HasForeignKey(sr => sr.ExamAttemptId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
