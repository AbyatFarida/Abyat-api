using Abyat.Domains.Models;

namespace Abyat.Domains.Contracts;

public interface ITestimonialImageQryRepo : ITableQryRepo<TbTestimonialImage>
{
    Task<List<TbTestimonialImage>> FindByTestimonialIdAsync(Guid testimonialId, CancellationToken cancellationToken = default);

}
