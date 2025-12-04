namespace Abyat.Bl.Dtos.Detailed;

public class SliderDetailedDto
{
    public Guid Id { get; set; }

    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public string? ButtonTextEn { get; set; }

    public string? ButtonTextAr { get; set; }

    public string? ButtonUrl { get; set; }

    public int Order { get; set; }

    public string ImageUrl { get; set; } = null!;

}