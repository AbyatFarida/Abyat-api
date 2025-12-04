namespace Abyat.Domains.Models;

public class TbSlider : BaseTable
{
    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public string? ButtonTextEn { get; set; }

    public string? ButtonTextAr { get; set; }

    public string? ButtonUrl { get; set; }

    public int Order { get; set; }

    public Guid? ImageSizeId { get; set; }


    #region Navigation Properties

    public virtual TbImageSize? ImageSize { get; set; } = null!;

    #endregion
}