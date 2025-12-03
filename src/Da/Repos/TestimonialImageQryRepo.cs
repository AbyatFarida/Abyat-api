using Abyat.Core.Exceptions;
using Abyat.Da.Context;
using Abyat.Da.Exceptions;
using Abyat.Da.Repos.Base.Factory;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Abyat.Da.Repos;

public class TestimonialImageQryRepo(
    IDbContextFactory<AbyatDbContext> contextFactory,
    ILogger<TableQryRepoWithFactory<TbTestimonialImage>> logger)
    : TableQryRepoWithFactory<TbTestimonialImage>(contextFactory, logger),
    ITestimonialImageQryRepo
{
    public async Task<List<TbTestimonialImage>> FindByTestimonialIdAsync(int testimonialId, CancellationToken cancellationToken)
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
                .Where(p => p.TestimonialId == testimonialId)
                .ToListAsync(cancellationToken);

            if (results == null || results.Count == 0)
                throw new NotFoundException($"No testimonial images found for testimonial ID {testimonialId}.");

            return results;
        }
        catch (Exception ex)
        {
            throw new DataAccessException(logger, ex, $"Failed to get testimonial images by testimonial ID {testimonialId}");
        }
    }


}
