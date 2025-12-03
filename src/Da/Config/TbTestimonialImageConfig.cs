using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbTestimonialImageConfig : IEntityTypeConfiguration<TbTestimonialImage>
{
    public void Configure(EntityTypeBuilder<TbTestimonialImage> builder)
    {
        builder.HasOne(d => d.Testimonial)
            .WithMany(p => p.TestimonialImages)
            .HasForeignKey(d => d.TestimonialId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(d => d.ImageSize)
            .WithMany()
            .HasForeignKey(d => d.ImageSizeId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasIndex(e => new { e.TestimonialId, e.ImageSizeId })
            .IsUnique();
    }
}
