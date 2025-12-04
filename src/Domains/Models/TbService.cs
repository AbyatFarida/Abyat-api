namespace Abyat.Domains.Models;

public class TbService : BaseTable
{
    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public string? ContentEn { get; set; }

    public string? ContentAr { get; set; }

    public string WhyEn { get; set; } = null!;

    public string WhyAr { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public Guid ServiceCategoryId { get; set; }

    public Guid ProcessId { get; set; }

    #region Navigation Properties

    public virtual TbServiceCategory ServiceCategory { get; set; } = null!;

    public virtual TbProcess Process { get; set; } = null!;

    public virtual ICollection<TbServiceFeature> ServiceFeatures { get; set; } = new List<TbServiceFeature>();

    public virtual ICollection<TbServiceProduct> ServiceProducts { get; set; } = new List<TbServiceProduct>();

    public virtual ICollection<TbServiceImage> ServiceImages { get; set; } = new List<TbServiceImage>();

    #endregion

}

