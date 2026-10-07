using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public class AdminAuditLogConfiguration
    : IEntityTypeConfiguration<AdminAuditLog>
{
    public void Configure(
        EntityTypeBuilder<AdminAuditLog> builder)
    {
        // Primary Key
        builder.HasKey(x => x.Id);


        builder.Property(x => x.ActionType)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.TableName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.RecordId)
            .IsRequired(false);

        builder.Property(x => x.OldValues)
            .HasColumnType("nvarchar(max)")
            .IsRequired(false);

        builder.Property(x => x.NewValues)
            .HasColumnType("nvarchar(max)")
            .IsRequired(false);

        builder.Property(x => x.IpAddress)
            .HasMaxLength(45)
            .IsRequired(false);

        builder.Property(x => x.Timestamp)
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder.HasOne(x => x.Admin)
            .WithMany(x => x.AdminAuditLogs)
            .HasForeignKey(x => x.AdminId)
            .OnDelete(DeleteBehavior.NoAction);


        // Indexes for faster searching
        builder.HasIndex(x => x.AdminId);

        builder.HasIndex(x => x.Timestamp);

        builder.HasIndex(x => new
        {
            x.TableName,
            x.RecordId
        });

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}