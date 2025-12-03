using Abyat.Bl.Contracts;
using Abyat.Bl.Contracts.Events;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos;
using Abyat.Bl.Services.Base;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using AutoMapper;
using Microsoft.SqlServer.Management.Smo.Wmi;
using static Abyat.Bl.Enums.ServicesEnums;

namespace Abyat.Bl.Services;

public class ProcessStepService(
    ITableQryRepo<TbProcessStep> repoQuery,
    ITableCmdRepo<TbProcessStep> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    ICrudPublisher publisher)
    : BaseService<TbProcessStep, ProcessStepDto>(repoQuery, repoCommand, mapper, userServiceQuery, publisher),
    IProcessStep
{
}
