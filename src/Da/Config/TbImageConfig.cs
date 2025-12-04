using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbImageConfig : IEntityTypeConfiguration<TbImage>
{
    public void Configure(EntityTypeBuilder<TbImage> builder)
    {
        builder.HasIndex(e => e.Slug)
            .IsUnique();

        builder.Property(e => e.Slug)
            .HasMaxLength(2000);

        builder.Property(e => e.Url)
            .HasMaxLength(2000);

    }
}