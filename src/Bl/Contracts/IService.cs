using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos;
using Abyat.Bl.Dtos.Detailed;
using Abyat.Domains.Contracts.Models;
using Abyat.Domains.Models;

namespace Abyat.Bl.Contracts;

public interface IService : IBaseService<TbService, ServiceDto>, IServiceImgs
{
    Task<(bool success, int id)> AddAsync(
        ServiceDto entity,
        List<int> featuresIds,
        List<int> productsIds,
        IEnumerable<int>? imageSizeIds = null,
        bool fireEvent = true);
    Task<PagedResult<ServiceDetailedDto>> GetPagedList(int pageNumber = 1, int pageSize = 50);
    Task<bool> UpdateAsync(
        ServiceDto entity,
        List<int> featuresIds,
        List<int> productsIds,
        IEnumerable<int>? imageSizeIds = null,
        bool fireEvent = true);

}