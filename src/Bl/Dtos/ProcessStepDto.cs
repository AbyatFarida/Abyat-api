using Abyat.Bl.Dtos.Base;

namespace Abyat.Bl.Dtos;

public class ProcessStepDto : BaseDto
{
    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string DescriptionEn { get; set; } = null!;

    public string DescriptionAr { get; set; } = null!;

    public int Order { get; set; }

    public int ProcessId { get; set; }

}