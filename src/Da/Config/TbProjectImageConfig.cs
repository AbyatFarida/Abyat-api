using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbProjectImageConfig : IEntityTypeConfiguration<TbProjectImage>
{
    public void Configure(EntityTypeBuilder<TbProjectImage> builder)
    {
        builder.HasOne(d => d.Project)
            .WithMany(p => p.ProjectImages)
            .HasForeignKey(d => d.ProjectId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(d => d.ImageSize)
            .WithMany(p => p.ProjectImage)
            .HasForeignKey(d => d.ImageSizeId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasIndex(e => new { e.ProjectId, e.ImageSizeId })
            .IsUnique();
    }
}
