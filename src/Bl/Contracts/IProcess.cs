using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos;
using Abyat.Domains.Models;

namespace Abyat.Bl.Contracts;

public interface IProcess : IBaseService<TbProcess, ProcessDto>
{
    Task<ProcessDto> GetByTitleAsync(string title);

}