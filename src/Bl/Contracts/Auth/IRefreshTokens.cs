
using Abyat.Bl.Dtos.User;

namespace Abyat.Bl.Contracts.Auth;

public interface IRefreshTokens
{
    public Task<bool> Refresh(RefreshTokenDto tokenDto);

    public Task<RefreshTokenDto> GetByToken(string token);

}
