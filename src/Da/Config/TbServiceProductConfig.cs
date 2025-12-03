using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbServiceProductConfig : IEntityTypeConfiguration<TbServiceProduct>
{
    public void Configure(EntityTypeBuilder<TbServiceProduct> builder)
    {
        builder.HasOne(d => d.Product)
            .WithMany(p => p.ServiceProducts)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(d => d.Service)
            .WithMany(p => p.ServiceProducts)
            .HasForeignKey(d => d.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.ProductId, e.ServiceId })
    .IsUnique();
    }
}
