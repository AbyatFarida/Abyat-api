using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbProcessConfig : IEntityTypeConfiguration<TbProcess>
{
    public void Configure(EntityTypeBuilder<TbProcess> builder)
    {
        builder.HasIndex(e => e.Title)
            .IsUnique();
    }
}