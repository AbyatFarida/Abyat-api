using Abyat.Bl.Contracts;
using Abyat.Bl.Contracts.Events;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos;
using Abyat.Bl.Services.Base;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using AutoMapper;
using static Abyat.Bl.Enums.ServicesEnums;

namespace Abyat.Bl.Services;

public class ProductService(
    ITableQryRepo<TbProduct> repoQuery,
    ITableCmdRepo<TbProduct> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    IProductImage productImage,
    ICrudPublisher publisher)
    : BaseService<TbProduct, ProductDto>(repoQuery, repoCommand, mapper, userServiceQuery, publisher),
    IProduct
{
    public async Task<(bool success, Guid id)> AddAsync(ProductDto entity, IEnumerable<Guid> imageSizeIds, bool fireEvent)
    {
        var add = await base.AddAsync(entity, fireEvent);

        if (imageSizeIds.Any())
        {
            foreach (var id in imageSizeIds)
            {
                if (await productImage.IsExistsAsync(p => p.ProductId == add.id && p.ImageSizeId == id))
                    continue;

                await productImage.AddAsync(new ProductImageDto
                {
                    ProductId = add.id,
                    ImageSizeId = id,
                });
            }
        }

        return (add.success, add.id);
    }

    public async Task<ImageDto>? GetFirstMedImg(Guid id)
    {
        if (!await HasImgs(id))
            return null;

        var images = await productImage.GetProductImgsAsync(id);

        if (images is null)
            return null;
        else
            return images.FirstOrDefault().MediumSize;
    }

    public async Task<bool> HasImgs(Guid id) => await productImage.IsExistsAsync(p => p.ProductId == id);

    public async Task<bool> UpdateAsync(ProductDto entity, IEnumerable<Guid> imageSizeIds, bool fireEvent)
    {
        var add = await base.UpdateAsync(entity, fireEvent);

        if (imageSizeIds.Any())
        {
            foreach (var id in imageSizeIds)
            {
                if (await productImage.IsExistsAsync(p => p.ProductId == entity.Id && p.ImageSizeId == id))
                    continue;

                await productImage.AddAsync(new ProductImageDto
                {
                    ProductId = entity.Id,
                    ImageSizeId = id,
                });
            }
        }

        return add;
    }

    public async Task<List<ImageSizeDto>> GetImgsAsync(Guid Id)
    {
        return await productImage.GetProductImgsAsync(Id);
    }

}