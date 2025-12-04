using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos;
using Abyat.Bl.Dtos.Detailed;
using Abyat.Domains.Contracts.Models;
using Abyat.Domains.Models;

namespace Abyat.Bl.Contracts;

public interface ITestimonial : IBaseService<TbTestimonial, TestimonialDto>, IServiceImgs
{
    Task<(bool success, Guid id)> AddAsync(TestimonialDto entity, IEnumerable<Guid> imageSizeIds, bool fireEvent = true);
    Task<PagedResult<TestimonialDetailedDto>> GetPagedList(int pageNumber = 1, int pageSize = 50);
    Task<bool> UpdateAsync(TestimonialDto entity, IEnumerable<Guid> imageSizeIds, bool fireEvent = true);
}