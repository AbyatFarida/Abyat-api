using Abyat.Bl.Contracts.Auth;
using Abyat.Bl.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Abyat.Api.Services.General;

public class TokenService(AppSettings appSettings) : ITokenService
{

    JwtSettings jwt = appSettings.Jwt;

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        string _secretKey = jwt.Key
            ?? throw new ArgumentNullException("Jwt:Key", "JWT secret key is missing in configuration.");

        if (string.IsNullOrWhiteSpace(_secretKey))
            throw new ArgumentException("JWT secret key cannot be empty or whitespace.", nameof(_secretKey));

        SymmetricSecurityKey? key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        SigningCredentials? credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
        issuer: jwt.Issuer,
        audience: jwt.Audience,
        claims: claims,
        notBefore: DateTime.UtcNow,
        expires: DateTime.UtcNow.AddMinutes(15),
        signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        byte[]? randomNumber = new byte[32];
        using (RandomNumberGenerator? rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        return Convert.ToBase64String(randomNumber);
    }

}
