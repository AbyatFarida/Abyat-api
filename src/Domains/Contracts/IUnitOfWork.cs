namespace Abyat.Domains.Contracts;

public interface IUnitOfWork : IAsyncDisposable
{
    ITableCmdRepo<Tb> Repository<Tb>() where Tb : BaseTable;
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
