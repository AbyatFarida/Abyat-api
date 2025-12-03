using Abyat.Bl.Dtos;

namespace Abyat.Bl.Contracts;

public interface IServiceImgs
{
    Task<ImageDto>? GetFirstMedImg(int id);

    Task<List<ImageSizeDto>> GetImgsAsync(int Id);

    Task<bool> HasImgs(int id);
}