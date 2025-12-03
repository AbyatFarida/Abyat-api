using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbProjectConfig : IEntityTypeConfiguration<TbProject>
{
    public void Configure(EntityTypeBuilder<TbProject> builder)
    {
        builder.HasIndex(e => e.TitleEn)
            .IsUnique();

        builder.HasIndex(e => e.TitleAr)
            .IsUnique();

        builder.HasIndex(d => d.Order)
            .IsUnique();

        builder.Property(e => e.DescriptionEn)
            .HasMaxLength(2000);

        builder.Property(e => e.DescriptionAr)
            .HasMaxLength(2000);

        builder.HasOne(d => d.Client)
            .WithMany(p => p.Projects)
            .HasForeignKey(d => d.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}