using Abyat.Bl.Dtos.Base;

namespace Abyat.Bl.Dtos;

public class CompanyDto : BaseDto
{
    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public string? AddressEn { get; set; }

    public string? AddressAr { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int? LogoId { get; set; }

}