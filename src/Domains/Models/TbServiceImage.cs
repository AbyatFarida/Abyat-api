namespace Abyat.Domains.Models;

public class TbServiceImage : BaseTable
{
    public Guid ServiceId { get; set; }

    public Guid ImageSizeId { get; set; }

    #region Navigation Properties

    public virtual TbImageSize ImageSize { get; set; } = null!;

    public virtual TbService Service { get; set; } = null!;

    #endregion

}