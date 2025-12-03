namespace Abyat.Domains.Models;

public class TbServiceProduct : BaseTable
{
    public int ServiceId { get; set; }

    public int ProductId { get; set; }

    #region Navigation Props

    public virtual TbProduct Product { get; set; } = null!;

    public virtual TbService Service { get; set; } = null!;

    #endregion

}