using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos;
using Abyat.Domains.Models;

namespace Abyat.Bl.Contracts;

public interface IServiceProduct : IBaseService<TbServiceProduct, ServiceProductDto>
{
    Task<List<ServiceProductDto>> GetByServiceIdAsync(int serviceId);
}
