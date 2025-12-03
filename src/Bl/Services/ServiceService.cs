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
using static Abyat.Core.Enums.Status.Status;

namespace Abyat.Bl.Services;

public class ServiceService(
    IUnitOfWork uow,
    ITableQryRepo<TbService> repoQry,
    IMapper mapper,
    IUserQry userServiceQry,
    ICrudPublisher publisher,
    IServiceFeature serviceFeatureService,
    IServiceProduct serviceProductService,
    IServiceImage serviceImage,
    IFeature featureService,
    IProduct productService)
    : BaseService<TbService, ServiceDto>(uow, repoQry, mapper, userServiceQry, publisher),
    IService
{
    public async Task<(bool success, int id)> AddAsync(
        ServiceDto entity,
        List<int> featuresIds,
        List<int> productsIds,
        IEnumerable<int>? imageSizeIds = null,
        bool fireEvent = true)
    {
        bool success = false;
        int id = -1;

        try
        {
            await uow.BeginTransactionAsync();

            (bool success, int id) add = await AddAsync(entity, fireEvent);

            success = add.success;
            id = add.id;

            if (imageSizeIds is not null && imageSizeIds.Any())
            {
                foreach (int imageSizeId in imageSizeIds)
                {
                    if (await serviceImage.IsExistsAsync(p => p.ServiceId == add.id && p.ImageSizeId == imageSizeId))
                        continue;

                    await serviceImage.AddAsync(new ServiceImageDto
                    {
                        ServiceId = add.id,
                        ImageSizeId = imageSizeId,
                    });
                }
            }

            if (!success)
                return (false, -1);

            if (featuresIds.Count > 0)
            {
                foreach (int featureId in featuresIds)
                {
                    FeatureDto? feature = await featureService.GetByIdAsync(featureId);
                    var featureAdd = await serviceFeatureService.AddAsync(new ServiceFeatureDto()
                    {
                        ServiceId = add.id,
                        FeatureId = feature.Id,
                    });

                    success = featureAdd.success;
                }
            }

            if (productsIds.Count > 0)
            {
                foreach (int productId in productsIds)
                {
                    ProductDto? product = await productService.GetByIdAsync(productId);
                    var productAdd = await serviceProductService.AddAsync(new ServiceProductDto()
                    {
                        ServiceId = add.id,
                        ProductId = product.Id,
                    });

                    success = productAdd.success;
                }
            }
            await uow.CommitAsync();
            return (success, id);
        }
        catch (Exception ex)
        {
            await uow.RollbackAsync();
            throw new Exception("Error in adding service", ex);
        }
    }

    public async Task<bool> UpdateAsync(
        ServiceDto entity,
        List<int> featuresIds,
        List<int> productsIds,
        IEnumerable<int>? imageSizeIds = null,
        bool fireEvent = true)
    {
        bool updateSuccess = await UpdateAsync(entity, fireEvent);
        if (!updateSuccess)
            return false;

        if (imageSizeIds is not null && imageSizeIds.Any())
        {
            foreach (int imgSizeId in imageSizeIds)
            {
                if (await serviceImage.IsExistsAsync(p => p.ServiceId == entity.Id && p.ImageSizeId == imgSizeId))
                    continue;

                await serviceImage.AddAsync(new ServiceImageDto
                {
                    ServiceId = entity.Id,
                    ImageSizeId = imgSizeId,
                });
            }
        }

        #region Lists Comparison Logic

        List<ServiceFeatureDto>? existingFeatures = await serviceFeatureService.GetByServiceIdAsync(entity.Id);
        List<int>? existingFeatureIds = existingFeatures.Select(f => f.FeatureId).ToList();
        List<int>? featuresToAdd = featuresIds.Except(existingFeatureIds).ToList();
        List<int>? featuresToRemove = existingFeatureIds.Except(featuresIds).ToList();

        List<ServiceProductDto>? existingProducts = await serviceProductService.GetByServiceIdAsync(entity.Id);
        List<int>? existingProductIds = existingProducts.Select(p => p.ProductId).ToList();
        List<int>? productsToAdd = productsIds.Except(existingProductIds).ToList();
        List<int>? productsToRemove = existingProductIds.Except(productsIds).ToList();

        #endregion

        try
        {
            await uow.BeginTransactionAsync();

            #region Handle Features

            var addFeatureTasks = featuresToAdd.Select(featureId =>
                serviceFeatureService.AddAsync(new ServiceFeatureDto
                {
                    ServiceId = entity.Id,
                    FeatureId = featureId
                })
            );
            var addFeatureResults = await Task.WhenAll(addFeatureTasks);
            bool addFeaturesSuccess = addFeatureResults.All(add => add.success);

            var removeFeatureTasks = featuresToRemove.Select(featureId =>
            {
                return serviceFeatureService.DeleteAsync(featureId);
            });
            var removeFeatureResults = await Task.WhenAll(removeFeatureTasks);
            bool removeFeaturesSuccess = removeFeatureResults.All(success => success);

            #endregion

            #region Handle Products

            var addProductTasks = productsToAdd.Select(productId =>
                serviceProductService.AddAsync(new ServiceProductDto
                {
                    ServiceId = entity.Id,
                    ProductId = productId
                })
            );
            var addProductResults = await Task.WhenAll(addProductTasks);
            bool addProductsSuccess = addProductResults.All(add => add.success);

            var removeProductTasks = productsToRemove.Select(productId =>
            {
                return serviceProductService.DeleteAsync(productId);
            });
            var removeProductResults = await Task.WhenAll(removeProductTasks);
            bool removeProductsSuccess = removeProductResults.All(success => success);

            #endregion

            await uow.CommitAsync();

            return updateSuccess && addFeaturesSuccess && removeFeaturesSuccess && addProductsSuccess && removeProductsSuccess;
        }
        catch (Exception ex)
        {
            await uow.RollbackAsync();
            throw new Exception("Error in updating service", ex);
        }
    }

    public async Task<ImageDto>? GetFirstMedImg(int id)
    {
        if (!await HasImgs(id))
            return null;

        var images = await serviceImage.GetServiceImgsAsync(id);

        if (images is null)
            return null;
        else
            return images.FirstOrDefault().MediumSize;
    }

    public async Task<bool> HasImgs(int id) => await serviceImage.IsExistsAsync(p => p.ServiceId == id);

    public async Task<List<ImageSizeDto>> GetImgsAsync(int Id)
    {
        return await serviceImage.GetServiceImgsAsync(Id);
    }

    public async Task<PagedResult<ServiceDetailedDto>> GetPagedList(int pageNumber = 1, int pageSize = 50)
    {
        return await repoQry.GetPagedListAsync(
            pageNumber,
            pageSize,
            p => p.CurrentState == enCurrentState.Active,
            p => new ServiceDetailedDto
            {
                Id = p.Id,
                TitleEn = p.TitleEn,
                TitleAr = p.TitleAr,
                DescriptionEn = p.DescriptionEn,
                DescriptionAr = p.DescriptionAr,
                ContentEn = p.ContentEn,
                ContentAr = p.ContentAr,
                WhyEn = p.WhyEn,
                WhyAr = p.WhyAr,
                Slug = p.Slug,
                ServiceCategory = new ServiceCategoryDetailedDto
                {
                    TitleEn = p.ServiceCategory.TitleEn,
                    TitleAr = p.ServiceCategory.TitleAr
                },
                ProcessSteps = p.Process.ProcessSteps
                    .Where(ps => ps.CurrentState == enCurrentState.Active)
                    .OrderBy(ps => ps.Order)
                    .Select(ps => new ProcessStepDetailedDto
                    {
                        Id = ps.Id,
                        TitleEn = ps.TitleEn,
                        TitleAr = ps.TitleAr,
                        DescriptionEn = ps.DescriptionEn,
                        DescriptionAr = ps.DescriptionAr,
                        Order = ps.Order,
                    })
                    .ToList(),
                Products = p.ServiceProducts
                .Where(sp => sp.CurrentState == enCurrentState.Active)
                .Select(sp => new ProductDetailedDto
                {
                    Id = sp.Id,
                    TitleEn = sp.Product.TitleEn,
                    TitleAr = sp.Product.TitleAr,
                    DescriptionEn = sp.Product.DescriptionEn,
                    DescriptionAr = sp.Product.DescriptionAr,
                    ImageUrl = sp.Product.ProductImages
                .Where(pi => pi.ImageSize.MediumSize != null)
                .Select(pi => pi.ImageSize.MediumSize.Url)
                .FirstOrDefault() ?? string.Empty
                })
                .ToList(),
                Features = p.ServiceFeatures
                    .Where(sf => sf.CurrentState == enCurrentState.Active)
                    .Select(sf => new FeatureDetailedDto
                    {
                        Id = sf.Id,
                        TitleEn = sf.Feature.TitleEn,
                        TitleAr = sf.Feature.TitleAr,
                    })
                    .ToList(),
                ImageUrl = p.ServiceImages.FirstOrDefault() != null ? p.ServiceImages.FirstOrDefault()!.ImageSize.MediumSize.Url : string.Empty,
            },
            p => p.CreatedAt,
            false,
            default,
            p => p.ServiceCategory,
            p => p.ServiceFeatures,
            p => p.ServiceImages,
            p => p.ServiceProducts,
            p => p.Process);
    }

}