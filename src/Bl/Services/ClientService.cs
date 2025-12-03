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

public class ClientService(
    ITableQryRepo<TbClient> repoQuery,
    ITableCmdRepo<TbClient> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    IProject project,
    ITestimonial testimonial,
    ICrudPublisher publisher)
    : BaseService<TbClient, ClientDto>(repoQuery, repoCommand, mapper, userServiceQuery, publisher),
    IClient
{
    public async Task<ClientDto?> GetByNameAsync(string name)
    {
        var lstClient = await repoQuery.FindAsync(x => x.NameEn == name);

        var client = lstClient.FirstOrDefault();

        return mapper.Map<ClientDto>(client);
    }

    public override async Task<bool> DeleteAsync(int id, enDeleteType deleteType = enDeleteType.HardDelete, bool fireEvent = true)
    {
        if (await project.IsExistsAsync(p => p.ClientId == id) ||
            await testimonial.IsExistsAsync(t => t.ClientId == id))
        {
            return false;
        }

        return await base.DeleteAsync(id, deleteType, fireEvent);
    }

}