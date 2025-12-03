using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos;
using Abyat.Bl.Dtos.Detailed;
using Abyat.Domains.Contracts.Models;
using Abyat.Domains.Models;

namespace Abyat.Bl.Contracts;

public interface ISlider : IBaseService<TbSlider, SliderDto>
{
    Task<PagedResult<SliderDetailedDto>> GetPagedList(int pageNumber = 1, int pageSize = 50);
}
