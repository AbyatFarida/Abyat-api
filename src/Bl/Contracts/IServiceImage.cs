using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos;
using Abyat.Domains.Models;

namespace Abyat.Bl.Contracts;

public interface IServiceImage : IBaseService<TbServiceImage, ServiceImageDto>
{
    Task<List<ImageSizeDto>> GetServiceImgsAsync(Guid serviceId);
}
