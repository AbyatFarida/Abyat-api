using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos;
using Abyat.Domains.Models;

namespace Abyat.Bl.Contracts;

public interface IServiceFeature : IBaseService<TbServiceFeature, ServiceFeatureDto>
{
    Task<List<ServiceFeatureDto>> GetByServiceIdAsync(int serviceId);
}
