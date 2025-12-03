using Abyat.Bl.Dtos.User;

namespace Abyat.Bl.Contracts.Auth;

public interface IUserCmdService
{
    Task<UserResultDto> CreateAdminAsync(CreateAdminDto registerDto);

    Task<UserResultDto> LoginAsync(LoginDto loginDto);

    Task LogoutAsync();

    Task<bool> ConfirmEmailAsync(string email, string token);

    Task<bool> SendPasswordResetTokenAsync(string email);

    Task<bool> ResetPasswordAsync(ResetPasswordDto model);

}
