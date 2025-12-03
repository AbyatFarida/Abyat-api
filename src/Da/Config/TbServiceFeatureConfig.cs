using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbServiceFeatureConfig : IEntityTypeConfiguration<TbServiceFeature>
{
    public void Configure(EntityTypeBuilder<TbServiceFeature> builder)
    {
        builder.HasOne(d => d.Service)
            .WithMany(p => p.ServiceFeatures)
            .HasForeignKey(d => d.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(d => d.Feature)
            .WithMany(p => p.ServiceFeatures)
            .HasForeignKey(d => d.FeatureId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.ServiceId, e.FeatureId })
          .IsUnique();
    }
}
