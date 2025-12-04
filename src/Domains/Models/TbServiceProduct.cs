namespace Abyat.Domains.Models;

public class TbServiceProduct : BaseTable
{
    public Guid ServiceId { get; set; }

    public Guid ProductId { get; set; }

    #region Navigation Props

    public virtual TbProduct Product { get; set; } = null!;

    public virtual TbService Service { get; set; } = null!;

    #endregion

}