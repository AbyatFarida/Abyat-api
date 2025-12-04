using Abyat.Bl.Dtos.Base;

namespace Abyat.Bl.Dtos;

public class TestimonialImageDto : BaseDto
{
    public Guid TestimonialId { get; set; }

    public Guid ImageSizeId { get; set; }

}