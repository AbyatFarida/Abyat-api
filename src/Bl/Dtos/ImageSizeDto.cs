using Abyat.Bl.Dtos.Base;
using Abyat.Domains.Models;

namespace Abyat.Bl.Dtos;

public class ImageSizeDto : BaseDto
{
    public int? SmallSizeId { get; set; } = null;
    public int MediumSizeId { get; set; } = -1;
    public int? LargeSizeId { get; set; } = null;

    public virtual ImageDto? SmallSize { get; set; } = null!;
    public virtual ImageDto MediumSize { get; set; } = null!;
    public virtual ImageDto? LargeSize { get; set; } = null!;

}