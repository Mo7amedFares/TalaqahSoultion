using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public class UserExamPolicyConfiguration : IEntityTypeConfiguration<UserExamPolicy>
{
    public void Configure(EntityTypeBuilder<UserExamPolicy> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.UserId, x.ExamId })
               .IsUnique()
               .HasDatabaseName("UX_UserExamPolicies_User_Exam");

        // User who receives the policy
        builder.HasOne(x => x.User)
               .WithMany(x => x.AssignedPolicies)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // Admin who assigned the policy
        builder.HasOne(x => x.AssignedByAdmin)
               .WithMany(x => x.CreatedPolicies)
               .HasForeignKey(x => x.AssignedByAdminId)
               .OnDelete(DeleteBehavior.Restrict);

        // Exam
        builder.HasOne(x => x.Exam)
               .WithMany(x => x.UserExamPolicies)
               .HasForeignKey(x => x.ExamId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
