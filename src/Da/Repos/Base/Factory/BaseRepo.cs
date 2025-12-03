using Abyat.Da.Context;
using Microsoft.EntityFrameworkCore;

namespace Abyat.Da.Repos.Base.Factory;

public abstract class BaseRepo<Tb> where Tb : class
{
    private readonly IDbContextFactory<AbyatDbContext> _contextFactory;

    protected BaseRepo(IDbContextFactory<AbyatDbContext> contextFactory)
    {
        _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
    }

    /// <summary>
    /// Creates a new DbContext and its corresponding DbSet for the given entity type.
    /// Automatically disposes the context when the returned tuple is disposed (via await using).
    /// </summary>
    protected async Task<(AbyatDbContext context, DbSet<Tb> dbSet)> CreateContextAndSetAsync(CancellationToken cancellationToken = default)
    {
        AbyatDbContext context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        DbSet<Tb> dbSet = context.Set<Tb>();
        return (context, dbSet);
    }
}