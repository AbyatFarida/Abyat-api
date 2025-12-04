using Abyat.Core.Exceptions;
using Abyat.Da.Context;
using Abyat.Da.Exceptions;
using Abyat.Da.Repos.Base.Factory;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Abyat.Da.Repos;

public class ServiceImageQryRepo(
    IDbContextFactory<AbyatDbContext> contextFactory,
    ILogger<TableQryRepoWithFactory<TbServiceImage>> logger)
    : TableQryRepoWithFactory<TbServiceImage>(contextFactory, logger),
    IServiceImageQryRepo
{
    public async Task<List<TbServiceImage>> FindByServiceIdAsync(Guid serviceId, CancellationToken cancellationToken)
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
                .Where(p => p.ServiceId == serviceId)
                .ToListAsync(cancellationToken);

            if (results == null || results.Count == 0)
                throw new NotFoundException($"No service images found for service ID {serviceId}.");

            return results;
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to get service images by service ID {serviceId}");
        }
    }


}
