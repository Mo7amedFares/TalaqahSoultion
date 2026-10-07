using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public class ExamScheduleConfiguration
    : IEntityTypeConfiguration<ExamSchedule>
{
    public void Configure(
        EntityTypeBuilder<ExamSchedule> builder)
    {
        builder.HasKey(x => x.Id);


        builder.HasOne(x => x.Exam)
            .WithMany(x => x.ExamSchedules)
            .HasForeignKey(x => x.ExamId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.Property(x => x.StartDate)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.IsActive)
             .HasColumnType("bit")
             .HasDefaultValue(true)
             .IsRequired();

        builder.Property(x => x.EndDate)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.StartTime)
            .HasColumnType("time")
            .IsRequired();


        builder.Property(x => x.EndTime)
            .HasColumnType("time")
            .IsRequired();


        builder.Property(x => x.DurationMinutes)
            .IsRequired();



        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}