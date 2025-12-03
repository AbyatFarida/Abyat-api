using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbProductImageConfig : IEntityTypeConfiguration<TbProductImage>
{
    public void Configure(EntityTypeBuilder<TbProductImage> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(d => d.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(d => d.ImageSize)
            .WithMany(a => a.ProductImages)
            .HasForeignKey(d => d.ImageSizeId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasIndex(e => new { e.ProductId, e.ImageSizeId })
            .IsUnique();

    }
}
