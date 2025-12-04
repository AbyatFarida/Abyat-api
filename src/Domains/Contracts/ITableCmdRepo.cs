using static Abyat.Core.Enums.Status.Status;

namespace Abyat.Domains.Contracts;

public interface ITableCmdRepo<Tb> where Tb : BaseTable
{
    #region Add Methods

    Task<(bool success, Guid newId)> AddAsync(Tb entity, CancellationToken cancellationToken = default);

    Task<(bool success, IEnumerable<Guid> newIds)> AddAsync(IEnumerable<Tb> entities, CancellationToken cancellationToken = default);

    #endregion

    #region Update Methods

    Task<bool> UpdateAsync(Tb entity, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(IEnumerable<Tb> entities, CancellationToken cancellationToken = default);

    #endregion

    #region Change Status Methods

    Task<bool> ChangeStatusAsync(Guid id, Guid userId, enCurrentState status = enCurrentState.Active, CancellationToken cancellationToken = default);

    Task<bool> ChangeStatusAsync(IEnumerable<Guid> id, Guid userId, enCurrentState status = enCurrentState.Active, CancellationToken cancellationToken = default);

    #endregion

    #region Delete Methods

    #region Hard Delete Methods

    Task<bool> HardDeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> HardDeleteAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    #endregion

    #region Soft Delete Methods

    Task<bool> SoftDeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    Task<bool> SoftDeleteAsync(IEnumerable<Guid> ids, Guid userId, CancellationToken cancellationToken = default);

    #endregion

    #endregion
}