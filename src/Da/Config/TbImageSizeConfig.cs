using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbImageSizeConfig : IEntityTypeConfiguration<TbImageSize>
{
    public void Configure(EntityTypeBuilder<TbImageSize> builder)
    {
        builder.HasOne(d => d.SmallSize)
            .WithMany(p => p.SmallSizeImageSizes)
            .HasForeignKey(d => d.SmallSizeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.MediumSize)
            .WithMany(p => p.MediumSizeImageSizes)
            .HasForeignKey(d => d.MediumSizeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.LargeSize)
            .WithMany(p => p.LargeSizeImageSizes)
            .HasForeignKey(d => d.LargeSizeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}