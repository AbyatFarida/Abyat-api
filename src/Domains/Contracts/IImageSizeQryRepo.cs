using Abyat.Domains.Models;

namespace Abyat.Domains.Contracts;

public interface IImageSizeQryRepo : ITableQryRepo<TbImageSize>
{
    Task<List<TbImageSize>> FindByImageIdAsync(Guid imageId, CancellationToken cancellationToken);
}
