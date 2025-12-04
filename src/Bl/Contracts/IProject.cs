using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos;
using Abyat.Bl.Dtos.Detailed;
using Abyat.Domains.Contracts.Models;
using Abyat.Domains.Models;
using static Abyat.Bl.Enums.ServicesEnums;

namespace Abyat.Bl.Contracts;

public interface IProject : IBaseService<TbProject, ProjectDto>, IServiceImgs
{
    Task<(bool success, Guid id)> AddAsync(ProjectDto entity, IEnumerable<Guid> imageSizeIds, bool fireEvent = true);
    Task<PagedResult<ProjectDetailedDto>> GetPagedList(int pageNumber = 1, int pageSize = 50);
    Task<bool> UpdateAsync(ProjectDto entity, IEnumerable<Guid> imageSizeIds, bool fireEvent = true);
}