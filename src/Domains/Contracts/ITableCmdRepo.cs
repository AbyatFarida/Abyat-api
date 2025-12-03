using static Abyat.Core.Enums.Status.Status;

namespace Abyat.Domains.Contracts;

public interface ITableCmdRepo<Tb> where Tb : BaseTable
{
    #region Add Methods

    Task<(bool success, int newId)> AddAsync(Tb entity, CancellationToken cancellationToken = default);

    Task<(bool success, IEnumerable<int> newIds)> AddAsync(IEnumerable<Tb> entities, CancellationToken cancellationToken = default);

    #endregion

    #region Update Methods

    Task<bool> UpdateAsync(Tb entity, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(IEnumerable<Tb> entities, CancellationToken cancellationToken = default);

    #endregion

    #region Change Status Methods

    Task<bool> ChangeStatusAsync(int id, int userId, enCurrentState status = enCurrentState.Active, CancellationToken cancellationToken = default);

    Task<bool> ChangeStatusAsync(IEnumerable<int> id, int userId, enCurrentState status = enCurrentState.Active, CancellationToken cancellationToken = default);

    #endregion

    #region Delete Methods

    #region Hard Delete Methods

    Task<bool> HardDeleteAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> HardDeleteAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);

    #endregion

    #region Soft Delete Methods

    Task<bool> SoftDeleteAsync(int id, int userId, CancellationToken cancellationToken = default);

    Task<bool> SoftDeleteAsync(IEnumerable<int> ids, int userId, CancellationToken cancellationToken = default);

    #endregion

    #endregion
}