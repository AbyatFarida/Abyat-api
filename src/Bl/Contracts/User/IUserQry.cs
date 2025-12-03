using Abyat.Bl.Dtos.User;
using System.Security.Claims;

namespace Abyat.Bl.Contracts.User;

public interface IUserQry
{
    Task<UserDto> GetByIdAsync(int userId);

    Task<UserDto> GetByEmailAsync(string email);

    int GetLoggedInUserId();

    Task<IEnumerable<string>> GetUserRolesAsync(int userId);

    Task<(Claim[] claims, UserDto user)> GetClaims(int userId);

    Task<(Claim[] claims, UserDto user)> GetClaims(string email);

}
