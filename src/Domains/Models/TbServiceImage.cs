namespace Abyat.Domains.Models;

public class TbServiceImage : BaseTable
{
    public int ServiceId { get; set; }

    public int ImageSizeId { get; set; }

    #region Navigation Properties

    public virtual TbImageSize ImageSize { get; set; } = null!;

    public virtual TbService Service { get; set; } = null!;

    #endregion

}