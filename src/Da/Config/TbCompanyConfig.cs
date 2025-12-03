using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbCompanyConfig : IEntityTypeConfiguration<TbCompany>
{
    public void Configure(EntityTypeBuilder<TbCompany> builder)
    {
        builder.HasIndex(e => e.NameEn)
            .IsUnique();

        builder.HasIndex(e => e.NameAr)
            .IsUnique();

        builder.Property(e => e.DescriptionEn)
            .HasMaxLength(2000);

        builder.Property(e => e.DescriptionAr)
            .HasMaxLength(2000);

        builder.HasOne(d => d.Logo)
            .WithMany(p => p.Companies)
            .HasForeignKey(d => d.LogoId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}


