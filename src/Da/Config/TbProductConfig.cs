using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbProductConfig : IEntityTypeConfiguration<TbProduct>
{
    public void Configure(EntityTypeBuilder<TbProduct> builder)
    {
        builder.Property(e => e.DescriptionEn)
         .HasMaxLength(2000);

        builder.Property(e => e.DescriptionAr)
            .HasMaxLength(2000);

        builder.HasIndex(e => e.TitleEn)
            .IsUnique();

        builder.HasIndex(e => e.TitleAr)
            .IsUnique();

    }
}