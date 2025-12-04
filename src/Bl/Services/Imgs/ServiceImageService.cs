using Abyat.Bl.Contracts;
using Abyat.Bl.Contracts.Events;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos;
using Abyat.Bl.Services.Base;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using AutoMapper;

namespace Abyat.Bl.Services.Imgs;

public class ServiceImageService(
    IServiceImageQryRepo repoQuery,
    ITableCmdRepo<TbServiceImage> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    ICrudPublisher publisher)
    : BaseService<TbServiceImage, ServiceImageDto>(repoQuery, repoCommand, mapper, userServiceQuery, publisher),
    IServiceImage
{

    public async Task<List<ImageSizeDto>> GetServiceImgsAsync(Guid serviceId)
    {
        var serviceImg = await repoQuery.FindByServiceIdAsync(serviceId);

        return mapper.Map<List<ImageSizeDto>>(serviceImg.Select(img => img.ImageSize).ToList());
    }


}

