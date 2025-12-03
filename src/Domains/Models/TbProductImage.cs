namespace Abyat.Domains.Models;

public class TbProductImage : BaseTable
{
    public int ProductId { get; set; }

    public int ImageSizeId { get; set; }

    #region Navigation Properties

    public virtual TbImageSize ImageSize { get; set; } = null!;

    public virtual TbProduct Product { get; set; } = null!;

    #endregion

}