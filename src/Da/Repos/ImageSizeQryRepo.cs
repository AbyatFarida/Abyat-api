using Abyat.Core.Exceptions;
using Abyat.Da.Context;
using Abyat.Da.Exceptions;
using Abyat.Da.Repos.Base.Factory;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Abyat.Da.Repos;

public class ImageSizeQryRepo(
    IDbContextFactory<AbyatDbContext> contextFactory,
    ILogger<TableQryRepoWithFactory<TbImageSize>> logger)
    : TableQryRepoWithFactory<TbImageSize>(contextFactory, logger),
    IImageSizeQryRepo
{
    public async Task<List<TbImageSize>> FindByImageIdAsync(int imageId, CancellationToken cancellationToken)
    {
        try
        {
            var (context, dbSet) = await CreateContextAndSetAsync(cancellationToken);

            var results = await dbSet
                .AsNoTracking()
                .Include(i => i.SmallSize)
                .Include(i => i.MediumSize)
                .Include(i => i.LargeSize)
                .Where(i => i.SmallSizeId == imageId || i.MediumSizeId == imageId || i.LargeSizeId == imageId)
                .ToListAsync(cancellationToken);

            if (results == null || results.Count == 0)
                throw new NotFoundException($"No image sizes found for image ID {imageId}.");

            return results;
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to retrieve image sizes by image ID {imageId}.");
        }
    }

    public override async Task<TbImageSize> FindAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var (context, dbSet) = await CreateContextAndSetAsync(cancellationToken);

            var results = await dbSet
                .AsNoTracking()
                .Include(i => i.SmallSize)
                .Include(i => i.MediumSize)
                .Include(i => i.LargeSize)
                .Where(i => i.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (results == null)
                throw new NotFoundException($"No image sizes found for ID {id}.");

            return results;
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to retrieve image sizes by ID {id}.");
        }
    }

}