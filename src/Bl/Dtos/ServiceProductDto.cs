using Abyat.Bl.Dtos.Base;

namespace Abyat.Bl.Dtos;

public class ServiceProductDto : BaseDto
{
    public Guid ServiceId { get; set; }

    public Guid ProductId { get; set; }

}