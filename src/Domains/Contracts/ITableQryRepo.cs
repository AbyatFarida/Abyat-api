using Abyat.Domains.Contracts.Models;
using System.Linq.Expressions;

namespace Abyat.Domains.Contracts;

public interface ITableQryRepo<Tb> where Tb : BaseTable
{
    Task<List<Tb>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Tb> FindAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Tb>> FindAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);

    Task<IEnumerable<Tb>> FindAsync(Expression<Func<Tb, bool>> filter, CancellationToken cancellationToken = default);

    Task<Tb?> GetFirstOrDefaultAsync(Expression<Func<Tb, bool>> filter, CancellationToken cancellationToken = default);

    Task<List<Tb>> GetListAsync(Expression<Func<Tb, bool>> filter, CancellationToken cancellationToken = default);

    Task<List<TResult>> GetListAsync<TResult>(Expression<Func<Tb, bool>>? filter = null,
        Expression<Func<Tb, TResult>>? selector = null,
        Expression<Func<Tb, object>>? orderBy = null,
        bool isDescending = false,
        CancellationToken cancellationToken = default,
        params Expression<Func<Tb, object>>[] includes);

    Task<PagedResult<Dto>> GetPagedListAsync<Dto>(
        int pageNumber,
        int pageSize,
        Expression<Func<Tb, bool>>? filter = null,
        Expression<Func<Tb, Dto>>? selector = null,
        Expression<Func<Tb, object>>? orderBy = null,
        bool isDescending = false,
        CancellationToken cancellationToken = default,
        params Expression<Func<Tb, object>>[] includes);

    Task<int> CountAsync(Expression<Func<Tb, bool>>? filter = null, CancellationToken cancellationToken = default);

    Task<bool> IsExistsAsync(Expression<Func<Tb, bool>> filter, CancellationToken cancellationToken = default);

    Task<bool> IsExistsAsync(int Id, CancellationToken cancellationToken = default);

}
