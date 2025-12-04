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

public class TestimonialService(
    ITableQryRepo<TbTestimonial> repoQuery,
    ITableCmdRepo<TbTestimonial> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    ITestimonialImage testimonialImage,
    ICrudPublisher publisher)
    : BaseService<TbTestimonial, TestimonialDto>(repoQuery, repoCommand, mapper, userServiceQuery, publisher),
    ITestimonial
{
    public async Task<(bool success, Guid id)> AddAsync(TestimonialDto entity, IEnumerable<Guid> imageSizeIds, bool fireEvent = true)
    {
        var add = await base.AddAsync(entity, fireEvent);

        if (imageSizeIds.Any())
        {
            foreach (var id in imageSizeIds)
            {
                if (await testimonialImage.IsExistsAsync(p => p.TestimonialId == add.id && p.ImageSizeId == id))
                    continue;

                await testimonialImage.AddAsync(new TestimonialImageDto
                {
                    TestimonialId = add.id,
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

        var images = await testimonialImage.GetTestimonialImgsAsync(id);

        if (images is null)
            return null;
        else
            return images.FirstOrDefault().MediumSize;
    }

    public async Task<bool> HasImgs(Guid id) => await testimonialImage.IsExistsAsync(p => p.TestimonialId == id);

    public async Task<bool> UpdateAsync(TestimonialDto entity, IEnumerable<Guid> imageSizeIds, bool fireEvent = true)
    {
        var add = await base.UpdateAsync(entity, fireEvent);

        if (imageSizeIds.Any())
        {
            foreach (var id in imageSizeIds)
            {
                if (await testimonialImage.IsExistsAsync(p => p.TestimonialId == entity.Id && p.ImageSizeId == id))
                    continue;

                await testimonialImage.AddAsync(new TestimonialImageDto
                {
                    TestimonialId = entity.Id,
                    ImageSizeId = id,
                });
            }
        }

        return add;
    }

    public async Task<List<ImageSizeDto>> GetImgsAsync(Guid Id)
    {
        return await testimonialImage.GetTestimonialImgsAsync(Id);
    }

    public async Task<PagedResult<TestimonialDetailedDto>> GetPagedList(int pageNumber = 1, int pageSize = 50)
    {
        return await repoQuery.GetPagedListAsync(
            pageNumber,
            pageSize,
            p => p.CurrentState == enCurrentState.Active,
            p => new TestimonialDetailedDto
            {
                Id = p.Id,
                TxtEn = p.TxtEn,
                TxtAr = p.TxtAr,
                Rating = p.Rating,
                ClientEn = p.Client.NameEn ?? string.Empty,
                ClientAr = p.Client.NameAr ?? string.Empty,
                CompanyEn = p.Client.Company.NameEn ?? string.Empty,
                CompanyAr = p.Client.Company.NameAr ?? string.Empty,
                ImageUrl = p.TestimonialImages.FirstOrDefault() != null ? p.TestimonialImages.FirstOrDefault()!.ImageSize.MediumSize.Url : string.Empty,
            },
            p => p.CreatedAt,
            false,
            default,
            p => p.Client, pageSize => pageSize.TestimonialImages);
    }

}