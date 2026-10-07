using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // 1. Primary Key
        builder.HasKey(e => e.Id);

        // 2. Core Identity Properties
        builder.Property(e => e.Email)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(e => e.PasswordHash)
            .HasMaxLength(256) 
            .IsRequired();

        builder.Property(e => e.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.LastName)
            .HasMaxLength(50)
            .IsRequired();

        // 3. System Auditing & Flags
        builder.Property(e => e.PermissionMask)
            .HasDefaultValue(0);

        // 4. Decoupled Enums (Mapped as highly readable strings)
        builder.Property(e => e.Role)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.UserType)
            .HasConversion<string>()
            .HasMaxLength(50);

        // 5. Conditional Institutional Fields
        builder.Property(e => e.NationalId)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.UniversityCode)
            .HasMaxLength(50);

        // 6. Business Logic Data Guard (Database Check Constraint combining Type & Role)
        // 6. Business Logic Data Guard (Database Check Constraint combining Type & Role)
        builder.ToTable("Users", t => t.HasCheckConstraint(
            "CK_User_Institution_Type",
            @"([UserType] = 'SchoolStudent' AND [SchoolId] IS NOT NULL AND [CollegeId] IS NULL) OR
              ([UserType] = 'CollegeStudent' AND [CollegeId] IS NOT NULL AND [SchoolId] IS NULL) OR
              ([UserType] = 'Public' AND [SchoolId] IS NULL AND [CollegeId] IS NULL) OR
              ([UserType] IS NULL AND [SchoolId] IS NULL AND [CollegeId] IS NULL)"
        ));

        // 7. Core Unique Indexes (Optimized with Null Filters)
        builder.HasIndex(e => e.Email)
            .IsUnique();

        builder.HasIndex(e => e.NationalId)
            .IsUnique();

        builder.HasIndex(e => e.UniversityCode)
            .IsUnique()
            .HasFilter("[UniversityCode] IS NOT NULL");

        // 8. High-Speed Performance Query Indexes for Foreign Keys
        builder.HasIndex(e => e.SchoolId);
        builder.HasIndex(e => e.CollegeId);

        // 9. Relational Foreign Key Mappings
        builder.HasOne(d => d.College)
            .WithMany(p => p.Users)
            .HasForeignKey(d => d.CollegeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(d => d.School)
            .WithMany(p => p.Users)
            .HasForeignKey(d => d.SchoolId)
            .OnDelete(DeleteBehavior.SetNull);

        // Self-referencing structural hierarchy tracking for SuperAdmin delegation
        builder.HasOne(d => d.PermissionGrantedBySuperAdmin)
            .WithMany()
            .HasForeignKey(d => d.PermissionGrantedBySuperAdminId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}