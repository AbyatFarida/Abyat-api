using Abyat.Bl.Dtos.Base;

namespace Abyat.Bl.Dtos;

public class ProjectDto : BaseDto
{
    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public string Slug { get; set; } = null!;

    public int Order { get; set; }

    public int? ClientId { get; set; }

}