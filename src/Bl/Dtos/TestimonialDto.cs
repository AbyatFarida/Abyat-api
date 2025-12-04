using Abyat.Bl.Dtos.Base;

namespace Abyat.Bl.Dtos;

public class TestimonialDto : BaseDto
{
    public string TxtEn { get; set; } = null!;

    public string TxtAr { get; set; } = null!;

    public int Rating { get; set; }

    public Guid ClientId { get; set; }

}