using Abyat.Api.Constants;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos.User;
using Abyat.Da.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Abyat.Api.Services.General;

public class UserCmdService(
    Bl.Contracts.Senders.IEmailSender emailSender,
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    RoleManager<AppRole> roleManager)
    : IUserCmd
{
    public async Task<UserResultDto> CreateAdminAsync(CreateAdminDto dto)
    {
        if (dto.Password != dto.ConfirmPassword)
            return Fail("Passwords do not match.");

        AppUser user = new AppUser
        {
            UserName = dto.UserName,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.Phone
        };

        IdentityResult? result = await userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            return Fail(result.Errors.Select(e => e.Description).ToList());
        }

        IdentityResult? roleResult = await userManager.AddToRoleAsync(user, SystemRoles.Admin);

        if (!roleResult.Succeeded)
        {
            return Fail(roleResult.Errors.Select(e => e.Description).ToList());
        }

        await SendConfirmationEmail(user);

        return new UserResultDto { Success = true };
    }

    private async Task SendConfirmationEmail(AppUser user)
    {
        string? emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);

        if (string.IsNullOrWhiteSpace(emailConfirmationToken))
            throw new ArgumentException("Email confirmation token cannot be null or empty.", nameof(emailConfirmationToken));


        if (string.IsNullOrWhiteSpace(user.Email))
            throw new ArgumentException("User email cannot be null or empty.", nameof(user.Email));

        string? tokenEncoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(emailConfirmationToken));

        string? body = $@"
<!DOCTYPE html>
<html>
<head>
  <meta charset='UTF-8'>
  <style>
    body {{
        font-family: Arial, sans-serif;
        background-color: #f9f9f9;
        color: #333;
        padding: 20px;
    }}
    .container {{
        max-width: 600px;
        margin: 0 auto;
        background: #ffffff;
        border-radius: 8px;
        padding: 30px;
        box-shadow: 0 2px 6px rgba(0,0,0,0.1);
    }}
    h2 {{
        color: #2c3e50;
    }}
    p {{
        font-size: 15px;
        line-height: 1.6;
    }}
    .token-box {{
        margin-top: 20px;
        padding: 12px;
        background-color: #f4f4f4;
        border: 1px solid #ccc;
        border-radius: 5px;
        font-family: monospace;
        font-size: 14px;
        color: #2c3e50;
        word-break: break-all;
    }}
    .footer {{
        margin-top: 25px;
        font-size: 12px;
        color: #888;
        text-align: center;
    }}
  </style>
</head>
<body>
  <div class='container'>
    <h2>Welcome to Elite UK</h2>
    <p>Hello {user.FirstName},</p>
    <p>Here is your verification token:</p>

    <div class='token-box'>{tokenEncoded}</div>

    <div class='footer'>
      <p>If you did not create this account, you can safely ignore this email.</p>
    </div>
  </div>
</body>
</html>";

        await emailSender.SendAsync(
      user.Email ?? string.Empty,
      "Confirm your email",
      body
  );
    }

    public async Task<UserResultDto> LoginAsync(LoginDto dto)
    {
        AppUser? user = await userManager.FindByEmailAsync(dto.Email);

        if (user == null || !await userManager.CheckPasswordAsync(user, dto.Password))
            return Fail("Invalid login.");

        if (!await userManager.IsEmailConfirmedAsync(user))
        {
            await SendConfirmationEmail(user);
            return Fail("Email not confirmed.");
        }

        if (await userManager.GetTwoFactorEnabledAsync(user))
            return new UserResultDto { Success = true, Requires2FA = true };

        return new UserResultDto
        {
            Success = true,
            Email = user.Email,
            UserId = user.Id
        };
    }

    public async Task LogoutAsync() => await signInManager.SignOutAsync();

    public async Task<bool> ConfirmEmailAsync(string email, string token)
    {
        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email)) return false;

        var user = await userManager.FindByEmailAsync(email);

        if (user == null) return false;

        var result = await userManager.ConfirmEmailAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token)));
        return result.Succeeded;
    }

    public async Task<bool> SendPasswordResetTokenAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("User email cannot be null or empty.", nameof(email));

        AppUser? user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return false;
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Reset token cannot be null or empty.", nameof(token));

        string? tokenEncoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        string? body = $@"
<!DOCTYPE html>
<html>
<head>
  <meta charset='UTF-8'>
  <style>
    body {{
        font-family: Arial, sans-serif;
        background-color: #f4f6f8;
        color: #333;
        padding: 20px;
    }}
    .container {{
        max-width: 600px;
        margin: 0 auto;
        background: #ffffff;
        border-radius: 8px;
        padding: 30px;
        box-shadow: 0 2px 8px rgba(0,0,0,0.1);
    }}
    h2 {{
        color: #c0392b;
    }}
    p {{
        font-size: 15px;
        line-height: 1.6;
    }}
    .token-box {{
        margin-top: 20px;
        padding: 12px;
        background-color: #f4f4f4;
        border: 1px solid #ccc;
        border-radius: 5px;
        font-family: monospace;
        font-size: 14px;
        color: #2c3e50;
        word-break: break-all;
    }}
    .footer {{
        margin-top: 25px;
        font-size: 12px;
        color: #888;
        text-align: center;
    }}
  </style>
</head>
<body>
  <div class='container'>
    <h2>Password Reset Requested</h2>
    <p>Hello,</p>
    <p>We received a request to reset the password for your account. Use the following token to complete the reset:</p>

    <div class='token-box'>{tokenEncoded}</div>

    <div class='footer'>
      <p>If you did not request this, you can safely ignore this email.</p>
    </div>
  </div>
</body>
</html>";

        await emailSender.SendAsync(email, "Reset Password", body);

        return true;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
    {
        AppUser? user = await userManager.FindByEmailAsync(dto.Email);

        if (user == null) return true;

        IdentityResult? result = await userManager.ResetPasswordAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(dto.Token)), dto.NewPassword);
        return result.Succeeded;
    }

    #region Helpers

    private static UserResultDto Fail(string error) => new UserResultDto { Success = false, Errors = new List<string> { error } };

    private static UserResultDto Fail(List<string> errors) => new UserResultDto { Success = false, Errors = errors };

    #endregion
}