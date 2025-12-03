using Abyat.Da.Context;
using Abyat.Domains.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Abyat.Da.Repos.Base.Factory;

public class UnitOfWorkWithFactory : IUnitOfWork, IAsyncDisposable, IDisposable
{
    private readonly IDbContextFactory<AbyatDbContext> _contextFactory;
    private readonly ILoggerFactory _loggerFactory;
    private readonly ConcurrentDictionary<Type, object> _repositories = new();
    private AbyatDbContext? _context;
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    public UnitOfWorkWithFactory(IDbContextFactory<AbyatDbContext> contextFactory, ILoggerFactory loggerFactory)
    {
        _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    private async Task<AbyatDbContext> GetContextAsync(CancellationToken cancellationToken = default)
    {
        _context ??= await _contextFactory.CreateDbContextAsync(cancellationToken);
        return _context;
    }

    /// <summary>
    /// Gets a command repository for the specified entity type.
    /// </summary>
    public ITableCmdRepo<Tb> Repository<Tb>() where Tb : BaseTable
    {
        return (ITableCmdRepo<Tb>)_repositories.GetOrAdd(typeof(Tb), _ =>
        {
            var cmdLogger = _loggerFactory.CreateLogger<TableCmdRepoWithFactory<Tb>>();
            var qryLogger = _loggerFactory.CreateLogger<TableQryRepoWithFactory<Tb>>();
            var queryRepo = new TableQryRepoWithFactory<Tb>(_contextFactory, qryLogger);
            return new TableCmdRepoWithFactory<Tb>(_contextFactory, cmdLogger, queryRepo);
        });
    }

    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var context = await GetContextAsync(cancellationToken);
        _transaction ??= await context.Database.BeginTransactionAsync(cancellationToken);
    }

    /// <summary>
    /// Commits the current transaction.
    /// </summary>
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_context is not null)
            await _context.SaveChangesAsync(cancellationToken);

        if (_transaction is not null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    /// <summary>
    /// Rolls back the current transaction.
    /// </summary>
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    /// <summary>
    /// Saves all pending changes without committing the transaction.
    /// </summary>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var context = await GetContextAsync(cancellationToken);
        return await context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Cleans up all disposable resources.
    /// </summary>
    public void Dispose()
    {
        DisposeAsync().AsTask().GetAwaiter().GetResult();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        _disposed = true;

        if (_transaction is not null)
            await _transaction.DisposeAsync();

        if (_context is not null)
            await _context.DisposeAsync();

        _repositories.Clear();
        GC.SuppressFinalize(this);
    }

}
