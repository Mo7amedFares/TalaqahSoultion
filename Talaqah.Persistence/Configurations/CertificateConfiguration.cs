using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Configurations;

public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
{
    public void Configure(EntityTypeBuilder<Certificate> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.CertificateCode).HasMaxLength(100).IsRequired();
        builder.Property(e => e.IssuedAt).HasDefaultValueSql("GETUTCDATE()");
        
        builder.HasIndex(e => e.CertificateCode)
               .IsUnique()
               .HasDatabaseName("UX_Certificates_Code");
               
               
        builder.HasOne(d => d.Student)
               .WithMany(p => p.Certificates)
               .HasForeignKey(d => d.StudentId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
