using Abyat.Da.Context;
using Abyat.Da.Exceptions;
using Abyat.Domains.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using static Abyat.Core.Enums.Status.Status;

namespace Abyat.Da.Repos.Base;

public class TableCmdRepo<Tb>(
    AbyatDbContext context,
    ILogger<TableCmdRepo<Tb>> logger,
    ITableQryRepo<Tb> repoQuery)
    : ITableCmdRepo<Tb>
    where Tb : BaseTable
{
    private readonly DbSet<Tb> dbSet = (context ?? throw new ArgumentNullException(nameof(context))).Set<Tb>();

    #region Add Methods

    public virtual async Task<(bool success, Guid newId)> AddAsync(Tb entity, CancellationToken cancellationToken = default)
    {
        const string methodName = nameof(AddAsync);

        if (entity == null)
        {
            logger.LogWarning(methodName, "Entity cannot be null");
            throw new ArgumentNullException(nameof(entity));
        }

        try
        {
            entity.Id = new Guid();
            entity.CreatedAt = DateTime.UtcNow;
            await dbSet.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return (true, entity.Id);
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to add entity of type {typeof(Tb).Name}");
        }
    }

    public virtual async Task<(bool success, IEnumerable<Guid> newIds)> AddAsync(IEnumerable<Tb> entities, CancellationToken cancellationToken = default)
    {
        const string methodName = nameof(AddAsync);

        if (entities == null)
        {
            logger.LogWarning(methodName, "Entities collection cannot be null");
            throw new ArgumentNullException(nameof(entities));
        }

        if (!entities.Any())
        {
            return (true, Enumerable.Empty<Guid>());
        }

        try
        {
            var utcNow = DateTime.UtcNow;
            var entityList = entities.ToList();

            foreach (var entity in entityList)
            {
                entity.Id = new Guid();
                entity.CreatedAt = utcNow;
            }

            await dbSet.AddRangeAsync(entityList, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return (true, entityList.Select(e => e.Id));
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to add entities of type {typeof(Tb).Name}");
        }
    }

    #endregion

    #region Update Methods

    public virtual async Task<bool> UpdateAsync(Tb entity, CancellationToken cancellationToken = default)
    {
        const string methodName = nameof(UpdateAsync);

        if (entity == null)
        {
            logger.LogWarning(methodName, "Entity cannot be null");
            throw new ArgumentNullException(nameof(entity));
        }

        try
        {
            var dbData = await repoQuery.FindAsync(entity.Id, cancellationToken);
            if (dbData == null)
            {
                logger.LogWarning(methodName, entity.Id);
                return false;
            }

            entity.CreatedAt = dbData.CreatedAt;
            entity.CreatedBy = dbData.CreatedBy;
            entity.UpdatedAt = DateTime.UtcNow;

            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            logger.LogWarning(methodName, $"Concurrency conflict updating entity {entity.Id}");
            throw new DataAccessException(logger, ex, $"Concurrency conflict updating entity {entity.Id}", isConcurrencyConflict: true);
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to update entity {entity.Id} of type {typeof(Tb).Name}");
        }
    }

    public virtual async Task<bool> UpdateAsync(IEnumerable<Tb> entities, CancellationToken cancellationToken = default)
    {
        const string methodName = nameof(UpdateAsync);

        if (entities == null)
        {
            logger.LogWarning(methodName, "Entities collection cannot be null");
            throw new ArgumentNullException(nameof(entities));
        }

        if (!entities.Any())
        {
            return true;
        }

        try
        {
            var entityList = entities.ToList();
            var ids = entityList.Select(e => e.Id).ToList();
            var utcNow = DateTime.UtcNow;

            // Get all existing entities in one query
            var existingEntities = await repoQuery.FindAsync(ids, cancellationToken);
            var existingEntitiesDict = existingEntities.ToDictionary(e => e.Id);
            // Check if all entities exist
            foreach (var entity in entityList)
            {
                if (!existingEntitiesDict.TryGetValue(entity.Id, out var dbData))
                {
                    logger.LogInformation(methodName, entity.Id);
                    return false;
                }

                entity.CreatedAt = dbData.CreatedAt;
                entity.CreatedBy = dbData.CreatedBy;
                entity.UpdatedAt = utcNow;

                context.Entry(entity).State = EntityState.Modified;
            }

            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            logger.LogWarning(methodName, $"Concurrency conflict updating entities");
            throw new DataAccessException(logger, ex, "Concurrency conflict updating entities", isConcurrencyConflict: true);
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to update entities of type {typeof(Tb).Name}");
        }
    }

    #endregion

    #region Change Status Methods

    public virtual async Task<bool> ChangeStatusAsync(Guid id, Guid userId, enCurrentState status, CancellationToken cancellationToken = default)
    {
        const string methodName = nameof(ChangeStatusAsync);

        try
        {
            var entity = await repoQuery.FindAsync(id, cancellationToken);
            if (entity == null)
            {
                logger.LogInformation(methodName, id);
                return false;
            }

            entity.CurrentState = status;
            entity.UpdatedBy = userId;
            entity.UpdatedAt = DateTime.UtcNow;

            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to change status for entity {id} of type {typeof(Tb).Name}");
        }
    }

    public virtual async Task<bool> ChangeStatusAsync(IEnumerable<Guid> ids, Guid userId, enCurrentState status = enCurrentState.Active, CancellationToken cancellationToken = default)
    {
        const string methodName = nameof(ChangeStatusAsync);

        if (ids == null)
        {
            logger.LogWarning(methodName, "Ids collection cannot be null");
            throw new ArgumentNullException(nameof(ids));
        }

        if (!ids.Any())
        {
            return true; // Consider empty collection as successful
        }

        try
        {

            var distinctIds = ids.Distinct().ToList();
            var entities = await dbSet
                .Where(e => distinctIds.Contains(e.Id))
                .ToListAsync(cancellationToken);

            // Check if all entities were found
            if (entities.Count != distinctIds.Count)
            {
                var foundIds = entities.Select(e => e.Id).ToList();
                var missingIds = distinctIds.Except(foundIds).ToList();

                logger.LogWarning(methodName, $"Some entities not found: {string.Join(", ", missingIds)}");
                return false;
            }

            var utcNow = DateTime.UtcNow;
            foreach (var entity in entities)
            {
                entity.CurrentState = status;
                entity.UpdatedBy = userId;
                entity.UpdatedAt = utcNow;
                context.Entry(entity).State = EntityState.Modified;
            }

            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex,
                $"Failed to change status for multiple entities of type {typeof(Tb).Name}");
        }
    }

    #endregion

    #region Delete Methods

    #region Hard Delete Methods

    public virtual async Task<bool> HardDeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        const string methodName = nameof(HardDeleteAsync);

        if (id != new Guid())
        {
            logger.LogWarning(methodName, "Entity ID must be greater than zero");
            throw new ArgumentException("Invalid entity ID", nameof(id));
        }

        try
        {
            var dbData = await repoQuery.FindAsync(id, cancellationToken);
            if (dbData == null)
            {
                logger.LogWarning(methodName, $"Entity with ID {id} not found");
                return false;
            }

            dbSet.Remove(dbData);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            logger.LogWarning(methodName, $"Concurrency conflict deleting entity {id}");
            throw new DataAccessException(logger, ex, $"Concurrency conflict deleting entity {id}", isConcurrencyConflict: true);
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to hard delete entity {id} of type {typeof(Tb).Name}");
        }
    }

    public virtual async Task<bool> HardDeleteAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        const string methodName = nameof(HardDeleteAsync);

        if (ids == null)
        {
            logger.LogWarning(methodName, "IDs collection cannot be null");
            throw new ArgumentNullException(nameof(ids));
        }

        var idList = ids.Where(id => id != new Guid()).Distinct().ToList();
        if (!idList.Any())
        {
            logger.LogWarning(methodName, "No valid IDs provided for deletion");
            return false;
        }

        try
        {
            // Fetch all existing entities in one query
            var existingEntities = await repoQuery.FindAsync(idList, cancellationToken);
            if (existingEntities == null || !existingEntities.Any())
            {
                logger.LogWarning(methodName, "No matching entities found to delete");
                return false;
            }

            dbSet.RemoveRange(existingEntities);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            logger.LogWarning(methodName, "Concurrency conflict deleting entities");
            throw new DataAccessException(logger, ex, "Concurrency conflict deleting entities", isConcurrencyConflict: true);
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to hard delete entities of type {typeof(Tb).Name}");
        }
    }

    #endregion

    #region Soft Delete Methods

    public virtual async Task<bool> SoftDeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        return await ChangeStatusAsync(id, userId, enCurrentState.Deleted, cancellationToken);
    }

    public virtual async Task<bool> SoftDeleteAsync(IEnumerable<Guid> ids, Guid userId, CancellationToken cancellationToken = default) => await ChangeStatusAsync(ids, userId, enCurrentState.Deleted, cancellationToken);

    #endregion

    #endregion
}