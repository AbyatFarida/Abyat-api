using Abyat.Bl.Enums;
using System.Linq.Expressions;

namespace Abyat.Bl.Contracts.Base;

/// <summary>
/// Provides a base contract for service operations with standard CRUD functionality
/// </summary>
/// <typeparam name="Tb">The entity type (typically representing a database entity)</typeparam>
/// <typeparam name="Dto">The Data Transfer Object type for input/output operations</typeparam>
public interface IBaseService<Tb, Dto>
{
    /// <summary>
    /// Retrieves all entities as a list of DTOs
    /// </summary>
    /// <returns>Task representing the asynchronous operation with a list of DTOs</returns>
    Task<List<Dto>> GetAllAsync();

    /// <summary>
    /// Retrieves a single entity by its unique identifier
    /// </summary>
    /// <param name="id">The int identifier of the entity</param>
    /// <returns>Task representing the asynchronous operation with the matching DTO</returns>
    Task<Dto> GetByIdAsync(Guid id);

    /// <summary>
    /// Adds a new entity based on the provided DTO
    /// </summary>
    /// <param name="entity">The DTO containing data for the new entity</param>
    /// <returns>
    /// Task representing the asynchronous operation with a tuple containing:
    /// success - indicates if the operation succeeded
    /// id - the int of the newly created entity (valid when success is true)
    /// </returns>
    Task<(bool success, Guid id)> AddAsync(Dto entity, bool fireEvent = true);

    /// <summary>
    /// Updates an existing entity based on the provided DTO
    /// </summary>
    /// <param name="entity">The DTO containing updated entity data</param>
    /// <returns>
    /// Task representing the asynchronous operation with a boolean indicating success
    /// </returns>
    Task<bool> UpdateAsync(Dto entity, bool fireEvent = true);

    /// <summary>
    /// Changes the status of an entity
    /// </summary>
    /// <param name="id">The int identifier of the entity</param>
    /// <param name="status">The new status value (default: 1)</param>
    /// <returns>
    /// Task representing the asynchronous operation with a boolean indicating success
    /// </returns>
    Task<bool> ActivateAsync(Guid id, bool fireEvent = true);

    Task<bool> DeactivateAsync(Guid id, bool fireEvent = true);

    Task<int> CountAsync(CancellationToken cancellationToken = default);

    Task<bool> IsExistsAsync(Expression<Func<Tb, bool>> filter, CancellationToken cancellationToken = default);

    Task<bool> IsExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, ServicesEnums.enDeleteType deleteType = ServicesEnums.enDeleteType.HardDelete, bool fireEvent = true);
}