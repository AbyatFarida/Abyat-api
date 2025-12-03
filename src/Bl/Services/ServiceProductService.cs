using Abyat.Bl.Contracts;
using Abyat.Bl.Contracts.Events;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos;
using Abyat.Bl.Services.Base;
using Abyat.Core.Enums.Status;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using AutoMapper;

namespace Abyat.Bl.Services;

public class ServiceProductService(
    ITableQryRepo<TbServiceProduct> repoQuery,
    ITableCmdRepo<TbServiceProduct> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    ICrudPublisher publisher)
    : BaseService<TbServiceProduct, ServiceProductDto>(repoQuery, repoCommand, mapper, userServiceQuery, publisher),
    IServiceProduct
{
    public async Task<List<ServiceProductDto>> GetByServiceIdAsync(int serviceId)
    {
        var lstProduct = await repoQuery.FindAsync(x => x.ServiceId == serviceId && x.CurrentState == Status.enCurrentState.Active);
        return mapper.Map<List<ServiceProductDto>>(lstProduct);
    }

}
