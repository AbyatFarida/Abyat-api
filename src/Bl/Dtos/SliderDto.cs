using Abyat.Bl.Dtos.Base;
using Abyat.Domains.Models;

namespace Abyat.Bl.Dtos;

public class SliderDto : BaseDto
{
    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public string? ButtonTextEn { get; set; }

    public string? ButtonTextAr { get; set; }

    public string? ButtonUrl { get; set; }

    public int Order { get; set; }

    public int? ImageSizeId { get; set; }

}