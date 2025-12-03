using Abyat.Bl.Dtos.Base;

namespace Abyat.Bl.Dtos;

public class ServiceDto : BaseDto
{
    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public string? ContentEn { get; set; }

    public string? ContentAr { get; set; }

    public string WhyEn { get; set; } = null!;

    public string WhyAr { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public int ServiceCategoryId { get; set; }

    public int ProcessId { get; set; }

}