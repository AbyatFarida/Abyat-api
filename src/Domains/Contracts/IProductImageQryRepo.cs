using Abyat.Domains.Models;

namespace Abyat.Domains.Contracts;

public interface IProductImageQryRepo : ITableQryRepo<TbProductImage>
{
    Task<List<TbProductImage>> FindByProductIdAsync(int productId, CancellationToken cancellationToken = default);

}