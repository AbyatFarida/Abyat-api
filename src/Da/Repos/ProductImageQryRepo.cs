using Abyat.Core.Exceptions;
using Abyat.Da.Context;
using Abyat.Da.Exceptions;
using Abyat.Da.Repos.Base.Factory;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Abyat.Da.Repos;

public class ProductImageQryRepo(
    IDbContextFactory<AbyatDbContext> contextFactory,
    ILogger<TableQryRepoWithFactory<TbProductImage>> logger)
    : TableQryRepoWithFactory<TbProductImage>(contextFactory, logger),
    IProductImageQryRepo
{
    public async Task<List<TbProductImage>> FindByProductIdAsync(Guid productId, CancellationToken cancellationToken)
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
                .Where(p => p.ProductId == productId)
                .ToListAsync(cancellationToken);

            if (results == null || results.Count == 0)
                throw new NotFoundException($"No product images found for product ID {productId}.");

            return results;
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to get product images by product ID {productId}");
        }
    }


}