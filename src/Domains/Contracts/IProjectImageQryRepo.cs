using Abyat.Domains.Models;

namespace Abyat.Domains.Contracts;

public interface IProjectImageQryRepo : ITableQryRepo<TbProjectImage>
{
    Task<List<TbProjectImage>> FindByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);

}