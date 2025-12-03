using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Dtos;
using Abyat.Domains.Models;
using Humanizer;

namespace Abyat.Bl.Contracts;

public interface IImageSize : IBaseService<TbImageSize, ImageSizeDto>
{
    Task<List<ImageSizeDto>> FindByImageIdAsync(int imageId, CancellationToken cancellationToken = default);

}