namespace Abyat.Domains.Models;

public class TbFeature : BaseTable
{
    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    #region Navigation Properties

    public virtual ICollection<TbServiceFeature> ServiceFeatures { get; set; } = new List<TbServiceFeature>();

    #endregion

}

