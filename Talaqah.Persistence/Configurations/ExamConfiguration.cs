using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public class ExamConfiguration : IEntityTypeConfiguration<Exam>
{
    public void Configure(EntityTypeBuilder<Exam> builder)
    {
        builder.HasKey(x => x.Id);


        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();


        builder.Property(x => x.ExamStatus)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();


        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);


        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();


        // Created By Admin
        builder.HasOne(x => x.CreatedByAdmin)
            .WithMany()
            .HasForeignKey(x => x.CreatedByAdminId)
            .OnDelete(DeleteBehavior.Restrict);



        // Updated By Admin
        builder.HasOne(x => x.UpdatedByAdmin)
            .WithMany()
            .HasForeignKey(x => x.UpdatedByAdminId)
            .OnDelete(DeleteBehavior.Restrict);



        // Exam -> Schedules
        builder.HasMany(x => x.ExamSchedules)
            .WithOne(x => x.Exam)
            .HasForeignKey(x => x.ExamId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(x => !x.IsDeleted);

    }
}
