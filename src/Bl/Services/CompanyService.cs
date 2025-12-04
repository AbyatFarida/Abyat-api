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

public class CompanyService(
    ITableQryRepo<TbCompany> repoQry,
    ITableCmdRepo<TbCompany> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    IClient client,
    ICrudPublisher publisher)
    : BaseService<TbCompany, CompanyDto>(repoQry, repoCommand, mapper, userServiceQuery, publisher),
    ICompany
{
    public async Task<CompanyDto?> GetByNameAsync(string name)
    {
        var lstCompany = await repoQry.FindAsync(x => x.NameEn == name);

        var company = lstCompany.FirstOrDefault();

        return mapper.Map<CompanyDto>(company);
    }

    public override async Task<bool> DeleteAsync(Guid id, enDeleteType deleteType = enDeleteType.HardDelete, bool fireEvent = true)
    {
        if (await client.IsExistsAsync(c => c.CompanyId == id))
        {
            return false;
        }

        return await base.DeleteAsync(id, deleteType, fireEvent);
    }

}