using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos.AuditLog;
using Abyat.Domains.Models;

namespace Abyat.Bl.Contracts.AuditLog;

public interface IAuditLog : IBaseService<TbAuditLog, AuditLogDto>
{

}