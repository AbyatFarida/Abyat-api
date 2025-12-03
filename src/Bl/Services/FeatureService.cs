using Abyat.Bl.Contracts;
using Abyat.Bl.Contracts.Events;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos;
using Abyat.Bl.Services.Base;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using AutoMapper;

namespace Abyat.Bl.Services;

public class FeatureService(
    ITableQryRepo<TbFeature> repoQuery,
    ITableCmdRepo<TbFeature> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    ICrudPublisher publisher)
    : BaseService<TbFeature, FeatureDto>(repoQuery, repoCommand, mapper, userServiceQuery, publisher),
    IFeature
{
}