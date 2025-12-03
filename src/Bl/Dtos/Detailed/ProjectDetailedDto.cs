namespace Abyat.Bl.Dtos.Detailed;

public class ProjectDetailedDto
{
    public int Id { get; set; }

    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public string Slug { get; set; } = null!;

    public string ClientEn { get; set; } = null!;

    public string ClientAr { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public int Order { get; set; }

}