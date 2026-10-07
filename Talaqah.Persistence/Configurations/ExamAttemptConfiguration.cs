using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public class ExamAttemptConfiguration : IEntityTypeConfiguration<ExamAttempt>
{
    public void Configure(EntityTypeBuilder<ExamAttempt> builder)
    {
        // 1. Table Name & Primary Key
        builder.ToTable("ExamAttempts");
        builder.HasKey(e => e.Id);

        // 2. Property Configurations
        builder.Property(e => e.FinalScore)
            .HasPrecision(5, 2);

        builder.Property(e => e.FinalCefrLevel)
            .HasMaxLength(2)
            .HasColumnType("varchar(2)")
            .IsRequired(false); // Make nullable if some attempts aren't graded yet

        builder.Property(e => e.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.StartedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired(false);

        // 3. Indexes & Constraints
        // Prevents the same student from having duplicate attempt numbers for a specific scheduled exam
        builder.HasIndex(e => new
        {
            e.StudentId,
            e.ExamScheduleId,
            e.AttemptNumber
        })
        .IsUnique() // Ensures uniqueness at the database level
        .HasDatabaseName("IX_ExamAttempts_Student_Schedule_Attempt");

        // 4. Relationships

        // ExamSchedule -> ExamAttempts (1-to-Many)
        builder.HasOne(e => e.ExamSchedule)
            .WithMany(s => s.ExamAttempts)
            .HasForeignKey(e => e.ExamScheduleId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent schedule deletion if attempts exist

        // Student -> ExamAttempts (1-to-Many)
        builder.HasOne(e => e.Student)
            .WithMany(u => u.ExamAttempts)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.NoAction); // Avoid cascade path conflicts

        // ExamAttempt -> StudentResponses (1-to-Many)
        builder.HasMany(e => e.StudentResponses)
            .WithOne(sr => sr.ExamAttempt)
            .HasForeignKey(sr => sr.ExamAttemptId)
            .OnDelete(DeleteBehavior.NoAction);

        // 5. Global Query Filters (Soft Delete handling)
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}