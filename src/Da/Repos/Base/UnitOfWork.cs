using Abyat.Da.Context;
using Abyat.Domains.Contracts;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Abyat.Da.Repos.Base;

public class UnitOfWork(AbyatDbContext ctx, ILoggerFactory loggerFactory) : IUnitOfWork
{
    private readonly ConcurrentDictionary<Type, object> _repositories = new();
    private IDbContextTransaction? _tx;

    public ITableCmdRepo<Tb> Repository<Tb>() where Tb : BaseTable
    {
        return (ITableCmdRepo<Tb>)_repositories.GetOrAdd(typeof(Tb), _ => new TableCmdRepo<Tb>(ctx, loggerFactory.CreateLogger<TableCmdRepo<Tb>>(), new TableQryRepo<Tb>(ctx, loggerFactory.CreateLogger<TableQryRepo<Tb>>())));
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default) => _tx = await ctx.Database.BeginTransactionAsync();

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await ctx.SaveChangesAsync();
        if (_tx is not null) await _tx.CommitAsync();
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default) => await _tx?.RollbackAsync()!;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => ctx.SaveChangesAsync();

    public async ValueTask DisposeAsync()
    {
        if (_tx is not null) await _tx.DisposeAsync();
        await ctx.DisposeAsync();
    }

}