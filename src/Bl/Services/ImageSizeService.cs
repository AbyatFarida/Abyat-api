using Abyat.Bl.Contracts;
using Abyat.Bl.Contracts.Events;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos;
using Abyat.Bl.Services.Base;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using AutoMapper;
using Humanizer;
using System.Threading;
using static Abyat.Bl.Enums.ServicesEnums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Abyat.Bl.Services;

public class ImageSizeService(
    IImageSizeQryRepo repoQuery,
    ITableCmdRepo<TbImageSize> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    IImage _ImageService,
    ICrudPublisher publisher)
    : BaseService<TbImageSize, ImageSizeDto>(repoQuery, repoCommand, mapper, userServiceQuery, publisher),
    IImageSize
{
    public async Task<List<ImageSizeDto>> FindByImageIdAsync(int imageId, CancellationToken cancellationToken = default)
    {
        var imgs = await repoQuery.FindByImageIdAsync(imageId, cancellationToken);
        return mapper.Map<List<ImageSizeDto>>(imgs);
    }

    public override async Task<ImageSizeDto> GetByIdAsync(int id)
    {
        var imgs = await repoQuery.FindAsync(id, default);
        return mapper.Map<ImageSizeDto>(imgs);
    }

    public override async Task<bool> DeleteAsync(int id, enDeleteType deleteType = enDeleteType.HardDelete, bool fireEvent = true)
    {
        bool success = true;

        var img = await GetByIdAsync(id);

        if (img == null) return true;

        success &= await base.DeleteAsync(id, deleteType, fireEvent);

        success &= await _ImageService.DeleteAsync(img.MediumSizeId, enDeleteType.HardDelete);

        if (img.SmallSizeId.HasValue && img.SmallSize is not null)
        {
            success &= await _ImageService.DeleteAsync(img.SmallSizeId.Value, enDeleteType.HardDelete);
        }

        if (img.LargeSizeId.HasValue && img.LargeSize is not null)
        {
            success &= await _ImageService.DeleteAsync(img.LargeSizeId.Value, enDeleteType.HardDelete);
        }

        return success;
    }

}