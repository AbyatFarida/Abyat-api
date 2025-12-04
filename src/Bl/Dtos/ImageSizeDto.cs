using Abyat.Bl.Dtos.Base;
using Abyat.Domains.Models;

namespace Abyat.Bl.Dtos;

public class ImageSizeDto : BaseDto
{
    public Guid? SmallSizeId { get; set; } = null;
    public Guid MediumSizeId { get; set; } = new Guid();
    public Guid? LargeSizeId { get; set; } = null;

    public virtual ImageDto? SmallSize { get; set; } = null!;
    public virtual ImageDto MediumSize { get; set; } = null!;
    public virtual ImageDto? LargeSize { get; set; } = null!;

}