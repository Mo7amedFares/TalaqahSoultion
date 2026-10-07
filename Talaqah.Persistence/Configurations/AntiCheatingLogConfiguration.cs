using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public class AntiCheatingLogConfiguration : IEntityTypeConfiguration<AntiCheatingLog>
{
    public void Configure(EntityTypeBuilder<AntiCheatingLog> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.EventType).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Details).IsRequired();
        builder.Property(e => e.Timestamp).HasDefaultValueSql("GETUTCDATE()");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
