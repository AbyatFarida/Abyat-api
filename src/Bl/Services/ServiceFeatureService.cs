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

public class ServiceFeatureService(
    ITableQryRepo<TbServiceFeature> repoQuery,
    ITableCmdRepo<TbServiceFeature> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    ICrudPublisher publisher)
    : BaseService<TbServiceFeature, ServiceFeatureDto>(repoQuery, repoCommand, mapper, userServiceQuery, publisher),
    IServiceFeature
{
    public async Task<List<ServiceFeatureDto>> GetByServiceIdAsync(Guid serviceId)
    {
        var lstFeatures = await repoQuery.FindAsync(x => x.ServiceId == serviceId && x.CurrentState == Status.enCurrentState.Active);

        return mapper.Map<List<ServiceFeatureDto>>(lstFeatures);
    }
}