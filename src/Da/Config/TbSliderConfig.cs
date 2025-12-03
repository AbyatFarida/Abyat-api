using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbSliderConfig : IEntityTypeConfiguration<TbSlider>
{
    public void Configure(EntityTypeBuilder<TbSlider> builder)
    {
        builder.Property(e => e.DescriptionEn)
            .HasMaxLength(2000);

        builder.Property(e => e.DescriptionAr)
            .HasMaxLength(2000);

        builder.HasIndex(e => e.TitleEn)
            .IsUnique();

        builder.HasIndex(e => e.TitleAr)
            .IsUnique();

        builder.HasIndex(e => e.Order)
            .IsUnique();

        builder.HasOne(d => d.ImageSize)
        .WithMany(p => p.Slider)
        .HasForeignKey(d => d.ImageSizeId)
        .OnDelete(DeleteBehavior.SetNull);

    }
}
