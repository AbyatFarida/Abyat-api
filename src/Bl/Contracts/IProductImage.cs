using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos;
using Abyat.Domains.Models;

namespace Abyat.Bl.Contracts;

public interface IProductImage : IBaseService<TbProductImage, ProductImageDto>
{
    Task<List<ImageSizeDto>> GetProductImgsAsync(Guid productId);
}
