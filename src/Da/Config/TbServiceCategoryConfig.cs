using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abyat.Da.Config;

public class TbServiceCategoryConfig : IEntityTypeConfiguration<TbServiceCategory>
{
    public void Configure(EntityTypeBuilder<TbServiceCategory> builder)
    {
        builder.HasIndex(e => e.TitleEn)
            .IsUnique();
        builder.HasIndex(e => e.TitleAr)
            .IsUnique();
    }
}

