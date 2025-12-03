using Abyat.Bl.Dtos.Base;
using Abyat.Domains.Models;

namespace Abyat.Bl.Dtos;

public class ProductDto : BaseDto
{
    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

}
