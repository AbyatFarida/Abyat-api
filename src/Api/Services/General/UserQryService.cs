using Abyat.Api.Constants;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos.User;
using Abyat.Da.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Abyat.Api.Services.General;

public class UserQryService(
    IMapper mapper,
    UserManager<AppUser> userManager,
    IHttpContextAccessor httpContextAccessor)
    : IUserQry
{
    public async Task<UserDto?> GetByIdAsync(int userId)
    {
        AppUser? user = await userManager.FindByIdAsync(userId.ToString());
        return user == null ? null : mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> GetByEmailAsync(string email)
    {
        AppUser? user = await userManager.FindByEmailAsync(email);
        return user == null ? null : mapper.Map<UserDto>(user);
    }

    public int GetLoggedInUserId() => int.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out int res) ? res : -1;

    #region Helpers

    private async Task<(Claim[] claims, UserDto user)> BuildUserClaims(UserDto user)
    {
        IEnumerable<string>? roles = await GetUserRolesAsync(user.Id);

        List<Claim>? claims = new List<Claim>
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email)
        };

        foreach (string role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        if (!roles.Contains(SystemRoles.User))
        {
            claims.Add(new Claim(ClaimTypes.Role, SystemRoles.User));
        }

        return (claims.ToArray(), user);
    }

    #endregion

    public async Task<IEnumerable<string>> GetUserRolesAsync(int userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            throw new InvalidOperationException($"User with ID '{userId}' was not found.");
        }

        return await userManager.GetRolesAsync(user);
    }

    public async Task<(Claim[] claims, UserDto user)> GetClaims(string email)
    {
        UserDto? user = await GetByEmailAsync(email);
        return await BuildUserClaims(user);
    }

    public async Task<(Claim[] claims, UserDto user)> GetClaims(int userId)
    {
        var user = await GetByIdAsync(userId);
        return await BuildUserClaims(user);
    }

}
