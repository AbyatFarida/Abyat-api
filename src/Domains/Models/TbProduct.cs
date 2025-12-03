namespace Abyat.Domains.Models;

public class TbProduct : BaseTable
{
    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }


    #region Navigation Properties

    public virtual ICollection<TbServiceProduct> ServiceProducts { get; set; } = new List<TbServiceProduct>();
    public virtual ICollection<TbProductImage> ProductImages { get; set; } = new List<TbProductImage>();

    #endregion

}
