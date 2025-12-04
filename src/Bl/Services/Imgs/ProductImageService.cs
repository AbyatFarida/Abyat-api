using Abyat.Bl.Contracts;
using Abyat.Bl.Contracts.Events;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos;
using Abyat.Bl.Services.Base;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using AutoMapper;

namespace Abyat.Bl.Services.Imgs;

public class ProductImageService(
    IProductImageQryRepo repoQuery,
    ITableCmdRepo<TbProductImage> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    ICrudPublisher publisher)
    : BaseService<TbProductImage, ProductImageDto>(repoQuery, repoCommand, mapper, userServiceQuery, publisher),
    IProductImage
{

    public async Task<List<ImageSizeDto>> GetProductImgsAsync(Guid productId)
    {
        var productImg = await repoQuery.FindByProductIdAsync(productId);

        return mapper.Map<List<ImageSizeDto>>(productImg.Select(img => img.ImageSize).ToList());
    }

}