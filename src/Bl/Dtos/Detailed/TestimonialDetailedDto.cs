namespace Abyat.Bl.Dtos.Detailed;

public class TestimonialDetailedDto
{
    public Guid Id { get; set; }

    public string TxtEn { get; set; } = null!;

    public string TxtAr { get; set; } = null!;

    public int Rating { get; set; }

    public string ClientEn { get; set; } = null!;

    public string ClientAr { get; set; } = null!;

    public string CompanyEn { get; set; } = null!;

    public string CompanyAr { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

}