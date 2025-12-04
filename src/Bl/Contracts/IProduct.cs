using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos;
using Abyat.Domains.Models;

namespace Abyat.Bl.Contracts;

public interface IProduct : IBaseService<TbProduct, ProductDto>, IServiceImgs
{
    Task<(bool success, Guid id)> AddAsync(ProductDto entity, IEnumerable<Guid> imageSizeIds, bool fireEvent = true);

    Task<bool> UpdateAsync(ProductDto entity, IEnumerable<Guid> imageSizeIds, bool fireEvent = true);
}