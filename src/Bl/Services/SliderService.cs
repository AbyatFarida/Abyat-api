using Abyat.Bl.Contracts;
using Abyat.Bl.Contracts.Events;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos;
using Abyat.Bl.Dtos.Detailed;
using Abyat.Bl.Services.Base;
using Abyat.Domains.Contracts;
using Abyat.Domains.Contracts.Models;
using Abyat.Domains.Models;
using AutoMapper;
using static Abyat.Bl.Enums.ServicesEnums;
using static Abyat.Core.Enums.Status.Status;

namespace Abyat.Bl.Services;

public class SliderService(
    ITableQryRepo<TbSlider> repoQuery,
    ITableCmdRepo<TbSlider> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    ICrudPublisher publisher)
    : BaseService<TbSlider, SliderDto>(repoQuery, repoCommand, mapper, userServiceQuery, publisher),
    ISlider
{
    public async Task<PagedResult<SliderDetailedDto>> GetPagedList(int pageNumber = 1, int pageSize = 50)
    {
        return await repoQuery.GetPagedListAsync(
            pageNumber,
            pageSize,
            p => p.CurrentState == enCurrentState.Active,
            p => new SliderDetailedDto
            {
                Id = p.Id,
                TitleEn = p.TitleEn,
                TitleAr = p.TitleAr,
                DescriptionEn = p.DescriptionEn,
                DescriptionAr = p.DescriptionAr,
                ButtonTextEn = p.ButtonTextEn,
                ButtonTextAr = p.ButtonTextAr,
                ButtonUrl = p.ButtonUrl,
                Order = p.Order,
                ImageUrl = p.ImageSize != null ? p.ImageSize.MediumSize.Url : string.Empty,
            },
            p => p.Order,
            false,
            default,
            p => p.ImageSize);
    }

}