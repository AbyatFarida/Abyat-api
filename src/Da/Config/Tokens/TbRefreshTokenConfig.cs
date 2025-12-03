
using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config.Tokens;

public class TbRefreshTokenConfig : IEntityTypeConfiguration<TbRefreshToken>
{
    public void Configure(EntityTypeBuilder<TbRefreshToken> builder)
    {
        builder.Property(e => e.Token)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasIndex(e => e.Token)
            .IsUnique()
            .HasDatabaseName("UQ_TbRefreshToken_Token");

        builder.Property(e => e.UserId).IsRequired();

        builder.Property(e => e.Expires).IsRequired();
    }
}
