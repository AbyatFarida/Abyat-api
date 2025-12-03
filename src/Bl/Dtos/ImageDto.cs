using Abyat.Bl.Dtos.Base;

namespace Abyat.Bl.Dtos;

public class ImageDto : BaseDto
{
    public string Slug { get; set; } = null!;

    public string Url { get; set; } = null!;

}