using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos;
using Abyat.Domains.Models;

namespace Abyat.Bl.Contracts;

public interface ITestimonialImage : IBaseService<TbTestimonialImage, TestimonialImageDto>
{
    Task<List<ImageSizeDto>> GetTestimonialImgsAsync(Guid testimonialId);
}