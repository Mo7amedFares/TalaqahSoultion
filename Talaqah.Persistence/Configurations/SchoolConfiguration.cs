using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public class SchoolConfiguration : IEntityTypeConfiguration<School>
{
    public void Configure(EntityTypeBuilder<School> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
