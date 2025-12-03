using Abyat.Bl.Contracts;
using Abyat.Bl.Contracts.Events;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos;
using Abyat.Bl.Services.Base;
using Abyat.Domains.Contracts;
using Abyat.Domains.Contracts.Models;
using Abyat.Domains.Models;
using AutoMapper;
using static Abyat.Bl.Enums.ServicesEnums;

namespace Abyat.Bl.Services;

public class ServiceCategoryService(
    ITableQryRepo<TbServiceCategory> repoQuery,
    ITableCmdRepo<TbServiceCategory> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    IService service,
    ICrudPublisher publisher)
    : BaseService<TbServiceCategory, ServiceCategoryDto>(repoQuery, repoCommand, mapper, userServiceQuery, publisher),
    IServiceCategory
{
    public async Task<ServiceCategoryDto> GetByTitleAsync(string title)
    {
        return mapper.Map<ServiceCategoryDto>(await repoQuery.GetFirstOrDefaultAsync(filter: (e => e.TitleEn == title)));
    }

    public override async Task<bool> DeleteAsync(int id, enDeleteType deleteType = enDeleteType.HardDelete, bool fireEvent = true)
    {
        if (await service.IsExistsAsync(s => s.ServiceCategoryId == id))
        {
            return false;
        }

        return await base.DeleteAsync(id, deleteType, fireEvent);
    }

}



