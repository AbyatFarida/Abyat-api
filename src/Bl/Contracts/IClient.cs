using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos;
using Abyat.Domains.Models;

namespace Abyat.Bl.Contracts;

public interface IClient : IBaseService<TbClient, ClientDto>
{
    Task<ClientDto?> GetByNameAsync(string NameEn);
}