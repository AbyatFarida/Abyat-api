using Abyat.Bl.Contracts;
using Abyat.Bl.Contracts.Events;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos;
using Abyat.Bl.Services.Base;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using AutoMapper;
using static Abyat.Bl.Enums.ServicesEnums;

namespace Abyat.Bl.Services;

public class ProcessService(
    ITableQryRepo<TbProcess> repoQry,
    ITableCmdRepo<TbProcess> repoCmd,
    IMapper mapper,
    IService service,
    IUserQry userServiceQry,
    IProcessStep processStep,
    ICrudPublisher publisher)
    : BaseService<TbProcess, ProcessDto>(repoQry, repoCmd, mapper, userServiceQry, publisher),
    IProcess
{
    public async Task<ProcessDto> GetByTitleAsync(string title)
    {
        var lstProcess = await repoQry.FindAsync(x => x.Title == title);

        return mapper.Map<ProcessDto>(lstProcess.FirstOrDefault());
    }

    public override async Task<bool> DeleteAsync(int id, enDeleteType deleteType = enDeleteType.HardDelete, bool fireEvent = true)
    {
        if (await service.IsExistsAsync(s => s.ProcessId == id) ||
            await processStep.IsExistsAsync(s => s.ProcessId == id))
        {
            return false;
        }

        return await base.DeleteAsync(id, deleteType, fireEvent);
    }

}