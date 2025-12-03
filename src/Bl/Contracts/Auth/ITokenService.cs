using System.Security.Claims;

namespace Abyat.Bl.Contracts.Auth;

public interface ITokenService
{
    public string GenerateAccessToken(IEnumerable<Claim> claims);

    public string GenerateRefreshToken();
}