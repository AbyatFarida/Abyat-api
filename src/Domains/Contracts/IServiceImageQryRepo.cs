using Abyat.Domains.Models;

namespace Abyat.Domains.Contracts;

public interface IServiceImageQryRepo : ITableQryRepo<TbServiceImage>
{
    Task<List<TbServiceImage>> FindByServiceIdAsync(int serviceId, CancellationToken cancellationToken = default);

}
