using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbServiceImageConfig : IEntityTypeConfiguration<TbServiceImage>
{
    public void Configure(EntityTypeBuilder<TbServiceImage> builder)
    {
        builder.HasOne(d => d.Service)
            .WithMany(p => p.ServiceImages)
            .HasForeignKey(d => d.ServiceId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(d => d.ImageSize)
            .WithMany(a => a.ServiceImage)
            .HasForeignKey(d => d.ImageSizeId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasIndex(e => new { e.ServiceId, e.ImageSizeId })
            .IsUnique();
    }
}
