namespace Abyat.Bl.Dtos.Detailed;

public class ProductDetailedDto
{
    public Guid Id { get; set; }

    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public string? ImageUrl { get; set; }

}
