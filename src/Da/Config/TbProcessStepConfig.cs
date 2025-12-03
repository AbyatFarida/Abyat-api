using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbProcessStepConfig : IEntityTypeConfiguration<TbProcessStep>
{
    public void Configure(EntityTypeBuilder<TbProcessStep> builder)
    {
        builder.HasOne(d => d.Process)
            .WithMany(p => p.ProcessSteps)
            .HasForeignKey(d => d.ProcessId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.DescriptionEn)
            .HasMaxLength(2000);

        builder.Property(e => e.DescriptionAr)
            .HasMaxLength(2000);

        builder.HasIndex(d => new { d.TitleEn, d.Order })
            .IsUnique();
        builder.HasIndex(d => new { d.TitleAr, d.Order })
            .IsUnique();
        builder.HasIndex(d => new { d.ProcessId, d.Order })
            .IsUnique();

    }
}