using Abyat.Bl.Dtos.Base;

namespace Abyat.Bl.Dtos;

public class ServiceImageDto : BaseDto
{
    public Guid ServiceId { get; set; }

    public Guid ImageSizeId { get; set; }

}