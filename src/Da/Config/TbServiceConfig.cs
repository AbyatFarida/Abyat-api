using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbServiceConfig : IEntityTypeConfiguration<TbService>
{
    public void Configure(EntityTypeBuilder<TbService> builder)
    {
        builder.HasIndex(e => e.TitleEn)
            .IsUnique();

        builder.HasIndex(e => e.TitleAr)
            .IsUnique();

        builder.HasIndex(e => e.Slug)
            .IsUnique();

        builder.Property(e => e.DescriptionEn)
          .HasMaxLength(2000);
        builder.Property(e => e.DescriptionAr)
          .HasMaxLength(2000);
        builder.Property(e => e.ContentEn)
          .HasMaxLength(2000);
        builder.Property(e => e.ContentAr)
          .HasMaxLength(2000);
        builder.Property(e => e.WhyEn)
          .HasMaxLength(2000);
        builder.Property(e => e.WhyAr)
          .HasMaxLength(2000);

        builder.HasOne(d => d.Process)
            .WithMany(p => p.Services)
            .HasForeignKey(d => d.ProcessId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.ServiceCategory)
            .WithMany(p => p.Services)
            .HasForeignKey(d => d.ServiceCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}