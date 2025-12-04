using Abyat.Bl.Dtos.Base;

namespace Abyat.Bl.Dtos;

public class ProjectImageDto : BaseDto
{
    public Guid ProjectId { get; set; }

    public Guid ImageSizeId { get; set; }

}