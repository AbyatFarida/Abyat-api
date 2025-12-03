using Abyat.Core.Exceptions;
using Abyat.Da.Context;
using Abyat.Da.Exceptions;
using Abyat.Domains.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Utils.Logging;

namespace Abyat.Da.Repos.Base;

public class ViewRepo<Vw>(AbyatDbContext context, ILogger<ViewRepo<Vw>> logger) : IViewRepo<Vw> where Vw : class
{
    private readonly DbSet<Vw> _dbSet = (context ?? throw new ArgumentNullException(nameof(context))).Set<Vw>();

    public async Task<IReadOnlyList<Vw>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }
        catch (Exception ex) when (ex is not DataAccessException)
        {
            throw new DataAccessException(logger, ex, $"Failed to retrieve all items of type {typeof(Vw).Name}");
        }
    }

    public async Task<Vw> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _dbSet.FindAsync(new object[] { id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(typeof(Vw).Name, id);
            }

            return entity;
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to retrieve item of type {typeof(Vw).Name} with ID {id}");
        }
    }

    public async Task<Vw> GetFirstOrDefaultAsync(Expression<Func<Vw, bool>> filter, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _dbSet.Where(filter).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            return entity;
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to retrieve first matching item of type {typeof(Vw).Name}");
        }
    }

    public async Task<IReadOnlyList<Vw>> GetListAsync(Expression<Func<Vw, bool>> filter, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _dbSet.Where(filter).AsNoTracking().ToListAsync(cancellationToken);
        }
        catch (Exception ex) when (ex is not DataAccessException)
        {
            throw new DataAccessException(logger, ex, $"Failed to retrieve filtered list of type {typeof(Vw).Name}");
        }
    }

}