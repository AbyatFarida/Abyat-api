using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbTestimonialConfig : IEntityTypeConfiguration<TbTestimonial>
{
    public void Configure(EntityTypeBuilder<TbTestimonial> builder)
    {
        builder.HasOne(d => d.Client)
            .WithMany(p => p.Testimonials)
            .HasForeignKey(d => d.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.TxtEn)
        .HasMaxLength(2000);

        builder.Property(e => e.TxtAr)
        .HasMaxLength(2000);

    }
}


