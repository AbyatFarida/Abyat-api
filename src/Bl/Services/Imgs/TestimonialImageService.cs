using Abyat.Bl.Contracts;
using Abyat.Bl.Contracts.Events;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Dtos;
using Abyat.Bl.Services.Base;
using Abyat.Domains.Contracts;
using Abyat.Domains.Models;
using AutoMapper;

namespace Abyat.Bl.Services.Imgs;

public class TestimonialImageService(
    ITestimonialImageQryRepo repoQuery,
    ITableCmdRepo<TbTestimonialImage> repoCommand,
    IMapper mapper,
    IUserQry userServiceQuery,
    ICrudPublisher publisher)
    : BaseService<TbTestimonialImage, TestimonialImageDto>(repoQuery, repoCommand, mapper, userServiceQuery, publisher),
    ITestimonialImage
{

    public async Task<List<ImageSizeDto>> GetTestimonialImgsAsync(Guid TestimonialId)
    {
        var TestimonialImg = await repoQuery.FindByTestimonialIdAsync(TestimonialId);

        return mapper.Map<List<ImageSizeDto>>(TestimonialImg.Select(img => img.ImageSize).ToList());
    }

}
