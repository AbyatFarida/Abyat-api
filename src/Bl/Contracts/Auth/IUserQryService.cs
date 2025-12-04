using Abyat.Bl.Dtos.User;
using System.Security.Claims;

namespace Abyat.Bl.Contracts.Auth;

public interface IUserQryService
{
    Task<UserDto> GetByIdAsync(Guid userId);

    Task<UserDto> GetByEmailAsync(string email);

    Guid GetLoggedInUserId();

    Task<IEnumerable<string>> GetUserRolesAsync(Guid userId);

    Task<(Claim[] claims, UserDto user)> GetClaims(Guid userId);

    Task<(Claim[] claims, UserDto user)> GetClaims(string email);
}
