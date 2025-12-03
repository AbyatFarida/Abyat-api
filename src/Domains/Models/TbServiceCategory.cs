namespace Abyat.Domains.Models;

public class TbServiceCategory : BaseTable
{
    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    #region Navigation Properties

    public virtual ICollection<TbService> Services { get; set; } = new List<TbService>();

    #endregion

}
