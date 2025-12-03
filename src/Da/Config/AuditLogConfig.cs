using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class AuditLogConfig : IEntityTypeConfiguration<TbAuditLog>
{
    public void Configure(EntityTypeBuilder<TbAuditLog> builder)
    {

        builder.Property(e => e.Details)
            .HasMaxLength(int.MaxValue);

    }
}