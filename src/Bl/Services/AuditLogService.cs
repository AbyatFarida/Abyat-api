using Abyat.Bl.Contracts.AuditLog;
using Abyat.Bl.Contracts.Events;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos.AuditLog;
using Abyat.Bl.Services.Base;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using AutoMapper;

namespace Abyat.Bl.Services;

public class AuditLogService(
    ITableQryRepo<TbAuditLog> repoQuery,
    ITableCmdRepo<TbAuditLog> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    ICrudPublisher publisher)
    : BaseService<TbAuditLog, AuditLogDto>(repoQuery, repoCommand, mapper, userServiceQuery, publisher),
    IAuditLog
{ }