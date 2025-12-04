using Abyat.Bl.Dtos.Base;

namespace Abyat.Bl.Dtos;

public class ProductImageDto : BaseDto
{
    public Guid ProductId { get; set; }

    public Guid ImageSizeId { get; set; }

}