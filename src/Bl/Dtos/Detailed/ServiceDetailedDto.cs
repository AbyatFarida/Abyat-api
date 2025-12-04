namespace Abyat.Bl.Dtos.Detailed;

public class ServiceDetailedDto
{
    public Guid Id { get; set; }

    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public string? ContentEn { get; set; }

    public string? ContentAr { get; set; }

    public string WhyEn { get; set; } = null!;

    public string WhyAr { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public ServiceCategoryDetailedDto ServiceCategory { get; set; } = null!;

    public List<ProcessStepDetailedDto> ProcessSteps { get; set; } = null!;

    public List<ProductDetailedDto> Products { get; set; } = null!;

    public List<FeatureDetailedDto> Features { get; set; } = null!;

}

