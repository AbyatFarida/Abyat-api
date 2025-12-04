using Abyat.Bl.Dtos;

namespace Abyat.Bl.Contracts;

public interface IServiceImgs
{
    Task<ImageDto>? GetFirstMedImg(Guid id);

    Task<List<ImageSizeDto>> GetImgsAsync(Guid Id);

    Task<bool> HasImgs(Guid id);
}