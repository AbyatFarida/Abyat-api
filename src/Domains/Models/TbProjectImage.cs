namespace Abyat.Domains.Models;

public class TbProjectImage : BaseTable
{
    public Guid ProjectId { get; set; }

    public Guid ImageSizeId { get; set; }

    #region Navigation Properties

    public virtual TbImageSize ImageSize { get; set; } = null!;

    public virtual TbProject Project { get; set; } = null!;

    #endregion

}