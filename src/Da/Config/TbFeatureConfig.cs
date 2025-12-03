using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbFeatureConfig : IEntityTypeConfiguration<TbFeature>
{
    public void Configure(EntityTypeBuilder<TbFeature> builder)
    {
        builder.HasIndex(e => e.TitleEn)
    .IsUnique();

        builder.HasIndex(e => e.TitleAr)
            .IsUnique();
    }
}


