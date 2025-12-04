namespace Abyat.Domains.Models;

public class TbProductImage : BaseTable
{
    public Guid ProductId { get; set; }

    public Guid ImageSizeId { get; set; }

    #region Navigation Properties

    public virtual TbImageSize ImageSize { get; set; } = null!;

    public virtual TbProduct Product { get; set; } = null!;

    #endregion

}