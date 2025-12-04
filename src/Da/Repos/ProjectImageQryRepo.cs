using Abyat.Core.Exceptions;
using Abyat.Da.Context;
using Abyat.Da.Exceptions;
using Abyat.Da.Repos.Base.Factory;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Abyat.Da.Repos;

public class ProjectImageQryRepo(
    IDbContextFactory<AbyatDbContext> contextFactory,
    ILogger<TableQryRepoWithFactory<TbProjectImage>> logger)
    : TableQryRepoWithFactory<TbProjectImage>(contextFactory, logger),
    IProjectImageQryRepo
{
    public async Task<List<TbProjectImage>> FindByProjectIdAsync(Guid projectId, CancellationToken cancellationToken)
    {
        try
        {
            var (context, dbSet) = await CreateContextAndSetAsync(cancellationToken);

            var results = await dbSet
                .AsNoTracking()
                .Include(p => p.ImageSize)
                    .ThenInclude(i => i.SmallSize)
                .Include(p => p.ImageSize)
                    .ThenInclude(i => i.MediumSize)
                .Include(p => p.ImageSize)
                    .ThenInclude(i => i.LargeSize)
                .Where(p => p.ProjectId == projectId)
                .ToListAsync(cancellationToken);

            if (results == null || results.Count == 0)
                throw new NotFoundException($"No project images found for project ID {projectId}.");

            return results;
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to get project images by project ID {projectId}");
        }
    }

}