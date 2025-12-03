using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbClientConfig : IEntityTypeConfiguration<TbClient>
{
    public void Configure(EntityTypeBuilder<TbClient> builder)
    {
        builder.HasOne(d => d.Company)
            .WithMany(p => p.Clients)
            .HasForeignKey(d => d.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}