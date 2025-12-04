using Abyat.Core.Exceptions;
using Abyat.Da.Context;
using Abyat.Da.Exceptions;
using Abyat.Domains.Contracts;
using Abyat.Domains.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;

namespace Abyat.Da.Repos.Base;

public class TableQryRepo<Tb>(
    AbyatDbContext context,
    ILogger<TableQryRepo<Tb>> logger)
    : ITableQryRepo<Tb>
    where Tb : BaseTable
{
    private readonly DbSet<Tb> dbSet = (context ?? throw new ArgumentNullException(nameof(context))).Set<Tb>();

    public virtual async Task<Tb> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            Tb? result = await dbSet.Where(a => a.Id == id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

            if (result == null)
            {
                throw new NotFoundException();
            }

            return result;
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to get entity by ID {id} of type {typeof(Tb).Name}");
        }
    }

    public virtual async Task<IEnumerable<Tb>> FindAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        if (ids == null)
        {
            logger.LogWarning(nameof(FindAsync), "Ids collection cannot be null");
            throw new ArgumentNullException(nameof(ids));
        }

        if (!ids.Any())
        {
            return Enumerable.Empty<Tb>();
        }

        try
        {
            return await dbSet.Where(e => ids.Distinct().ToList().Contains(e.Id)).AsNoTracking().ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to find multiple entities of type {typeof(Tb).Name}");
        }
    }

    public virtual async Task<IEnumerable<Tb>> FindAsync(Expression<Func<Tb, bool>> predicate, CancellationToken cancellationToken = default)
    {
        if (predicate == null)
        {
            logger.LogWarning(nameof(FindAsync), "Predicate cannot be null");
            throw new ArgumentNullException(nameof(predicate));
        }

        try
        {
            var lst = await dbSet.Where(predicate).AsNoTracking().ToListAsync(cancellationToken);

            if (lst.Count > 0)
            {
                return lst;
            }

            return new List<Tb>();
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to find entities using predicate for type {typeof(Tb).Name}");
        }
    }

    public virtual async Task<PagedResult<TResult>> GetPagedListAsync<TResult>(
        int pageNumber,
        int pageSize,
        Expression<Func<Tb, bool>>? filter = null,
        Expression<Func<Tb, TResult>>? selector = null,
        Expression<Func<Tb, object>>? orderBy = null,
        bool isDescending = false,
        CancellationToken cancellationToken = default,
        params Expression<Func<Tb, object>>[] includes)
    {
        const string methodName = nameof(GetPagedListAsync);

        if (pageNumber < 1)
        {
            logger.LogWarning(methodName, "Page number must be greater than 0");
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than 0");
        }

        if (pageSize < 1)
        {
            logger.LogWarning(methodName, "Page size must be greater than 0");
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0");
        }

        try
        {
            IQueryable<Tb> query = dbSet.AsQueryable();

            foreach (Expression<Func<Tb, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            int totalCount = await query.CountAsync(cancellationToken);

            if (orderBy != null)
            {
                query = isDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            }

            query = query.AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize);

            List<TResult> items = selector != null ? await query.Select(selector).ToListAsync(cancellationToken) : await query.Cast<TResult>().ToListAsync(cancellationToken);

            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedResult<TResult>
            {
                Items = items,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to get paged list for type {typeof(Tb).Name}");
        }
    }

    public virtual async Task<List<Tb>> GetListAsync(Expression<Func<Tb, bool>> filter, CancellationToken cancellationToken = default)
    {
        try
        {
            return await dbSet.Where(filter).AsNoTracking().ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to get filtered list for type {typeof(Tb).Name}");
        }
    }

    public virtual async Task<List<TResult>> GetListAsync<TResult>(
        Expression<Func<Tb, bool>>? filter = null,
        Expression<Func<Tb, TResult>>? selector = null,
        Expression<Func<Tb, object>>? orderBy = null,
        bool isDescending = false,
        CancellationToken cancellationToken = default,
        params Expression<Func<Tb, object>>[] includes)
    {
        try
        {
            IQueryable<Tb> query = dbSet.AsQueryable();

            foreach (var include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = isDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

            query = query.AsNoTracking();

            List<TResult> items;

            if (selector != null)
                items = await query.Select(selector).ToListAsync(cancellationToken);
            else
                items = await query.Cast<TResult>().ToListAsync(cancellationToken);

            return items;
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to get projected list for type {typeof(Tb).Name}");
        }
    }

    public virtual async Task<List<Tb>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await dbSet.Where(a => a.CurrentState > 0).AsNoTracking().ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to get all entities of type {typeof(Tb).Name}");
        }
    }

    public virtual async Task<Tb?> GetFirstOrDefaultAsync(Expression<Func<Tb, bool>> filter, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await dbSet.Where(filter).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

            if (result == null)
            {
                throw new NotFoundException();
            }

            return result;
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed in GetFirstOrDefault for type {typeof(Tb).Name}");
        }
    }

    public virtual async Task<int> CountAsync(Expression<Func<Tb, bool>>? filter = null, CancellationToken cancellationToken = default)
    {
        try
        {
            IQueryable<Tb> query = dbSet.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            return await query.CountAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to count entities of type {typeof(Tb).Name}");
        }
    }

    public virtual async Task<bool> IsExistsAsync(Expression<Func<Tb, bool>> filter, CancellationToken cancellationToken = default)
    {
        try
        {
            return await dbSet.AnyAsync(filter, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed in existence check for type {typeof(Tb).Name}");
        }
    }

    public virtual async Task<bool> IsExistsAsync(Guid Id, CancellationToken cancellationToken = default) => await IsExistsAsync(x => x.Id == Id && x.CurrentState > 0, cancellationToken);

}