using Abyat.Domains.Models;

namespace Abyat.Domains.Contracts;

public interface IProductImageQryRepo : ITableQryRepo<TbProductImage>
{
    Task<List<TbProductImage>> FindByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

}