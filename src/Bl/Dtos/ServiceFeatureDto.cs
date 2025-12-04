using Abyat.Bl.Dtos.Base;

namespace Abyat.Bl.Dtos;

public class ServiceFeatureDto : BaseDto
{
    public Guid ServiceId { get; set; }

    public Guid FeatureId { get; set; }

}