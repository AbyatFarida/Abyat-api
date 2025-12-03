using System.Linq.Expressions;

namespace Abyat.Domains.Contracts;

public interface IViewRepo<Vw> where Vw : class
{
    Task<IReadOnlyList<Vw>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Vw> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Vw> GetFirstOrDefaultAsync(Expression<Func<Vw, bool>> filter, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Vw>> GetListAsync(Expression<Func<Vw, bool>> filter, CancellationToken cancellationToken = default);

}
