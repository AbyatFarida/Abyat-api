using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos;
using Abyat.Domains.Models;

namespace Abyat.Bl.Contracts;

public interface ICompany : IBaseService<TbCompany, CompanyDto>
{
    Task<CompanyDto?> GetByNameAsync(string name);
}