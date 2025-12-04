using Abyat.Da.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;
using Utils.Format;

namespace Abyat.Da.Context;

public partial class AbyatDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public AbyatDbContext(DbContextOptions<AbyatDbContext> options) : base(options) { }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveMaxLength(500);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(BaseTable).IsAssignableFrom(entityType.ClrType))
            {
                EntityTypeBuilder? entity = builder.Entity(entityType.ClrType);

                entity.HasKey(nameof(BaseTable.Id))
                    .HasName($"PK_{entityType.ClrType.Name}");

                entity.ToTable($"{FormatHelper.Pluralize(entityType.ClrType.Name)}");

                entity.HasKey(nameof(BaseTable.Id));

                entity.Property(nameof(BaseTable.Id))
                    .ValueGeneratedOnAdd();

                entity.Property(nameof(BaseTable.CreatedAt))
                    .IsRequired();
                entity.Property(nameof(BaseTable.UpdatedAt));
                entity.Property(nameof(BaseTable.CurrentState))
                    .IsRequired();

                entity.HasOne(typeof(AppUser))
                    .WithMany()
                    .HasForeignKey(nameof(BaseTable.CreatedBy))
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(typeof(AppUser))
                    .WithMany()
                    .HasForeignKey(nameof(BaseTable.UpdatedBy))
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            }
        }

        OnModelCreatingPartial(builder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}