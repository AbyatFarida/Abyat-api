using Abyat.Bl.Contracts;
using Abyat.Bl.Contracts.Events;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos;
using Abyat.Bl.Services.Base;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using AutoMapper;

namespace Abyat.Bl.Services.Imgs;

public class ProjectImageService(
    IProjectImageQryRepo repoQuery,
    ITableCmdRepo<TbProjectImage> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    ICrudPublisher publisher)
    : BaseService<TbProjectImage, ProjectImageDto>(repoQuery, repoCommand, mapper, userServiceQuery, publisher),
    IProjectImage
{

    public async Task<List<ImageSizeDto>> GetProjectImgsAsync(int projectId)
    {
        var projectImg = await repoQuery.FindByProjectIdAsync(projectId);

        return mapper.Map<List<ImageSizeDto>>(projectImg.Select(img => img.ImageSize).ToList());
    }

}