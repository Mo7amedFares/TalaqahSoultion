using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public class CollegeConfiguration : IEntityTypeConfiguration<College>
{
    public void Configure(EntityTypeBuilder<College> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
