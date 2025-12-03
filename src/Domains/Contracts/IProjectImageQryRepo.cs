using Abyat.Domains.Models;

namespace Abyat.Domains.Contracts;

public interface IProjectImageQryRepo : ITableQryRepo<TbProjectImage>
{
    Task<List<TbProjectImage>> FindByProjectIdAsync(int projectId, CancellationToken cancellationToken = default);

}