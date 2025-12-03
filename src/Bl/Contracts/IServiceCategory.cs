using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos;
using Abyat.Bl.Services;
using Abyat.Domains.Contracts.Models;
using Abyat.Domains.Models;

namespace Abyat.Bl.Contracts;

public interface IServiceCategory : IBaseService<TbServiceCategory, ServiceCategoryDto>
{
    Task<ServiceCategoryDto> GetByTitleAsync(string title);
}
