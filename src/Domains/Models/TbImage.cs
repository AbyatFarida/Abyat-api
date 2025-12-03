namespace Abyat.Domains.Models;

public class TbImage : BaseTable
{
    public string Slug { get; set; } = null!;

    public string Url { get; set; } = null!;

    #region Navigation Properties

    public virtual ICollection<TbImageSize> SmallSizeImageSizes { get; set; } = new List<TbImageSize>();
    public virtual ICollection<TbImageSize> MediumSizeImageSizes { get; set; } = new List<TbImageSize>();
    public virtual ICollection<TbImageSize> LargeSizeImageSizes { get; set; } = new List<TbImageSize>();

    #endregion

}