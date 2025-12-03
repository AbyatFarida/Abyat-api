using Abyat.Bl.Contracts.Auth;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos.User;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using AutoMapper;
using static Abyat.Core.Enums.Status.Status;

namespace Abyat.Bl.Services.User;

public class RefreshTokenService(
    ITableQryRepo<TbRefreshToken> repoQry,
    ITableCmdRepo<TbRefreshToken> repoCmd,
    IMapper mapper,
    IUserQry userQryService)
    : IRefreshTokens
{
    public async Task<bool> Refresh(RefreshTokenDto tokenDto)
    {
        List<TbRefreshToken>? tokenList = await repoQry.GetListAsync(a => a.UserId == tokenDto.UserId && a.CurrentState == enCurrentState.Active);

        foreach (TbRefreshToken dbToken in tokenList)
        {
            await repoCmd.ChangeStatusAsync(dbToken.Id, tokenDto.UserId, enCurrentState.InActive);
        }

        TbRefreshToken? dbTokens = mapper.Map<RefreshTokenDto, TbRefreshToken>(tokenDto);
        await repoCmd.AddAsync(dbTokens);
        return true;
    }

    public async Task<RefreshTokenDto> GetByToken(string token) => mapper.Map<TbRefreshToken, RefreshTokenDto>(await repoQry.GetFirstOrDefaultAsync(a => a.Token == token));

}
