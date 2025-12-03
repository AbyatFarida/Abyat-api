using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos;
using Abyat.Domains.Models;

namespace Abyat.Bl.Contracts;

public interface IProjectImage : IBaseService<TbProjectImage, ProjectImageDto>
{
    Task<List<ImageSizeDto>> GetProjectImgsAsync(int projectId);
}
