namespace Abyat.Domains.Models;

public class TbProject : BaseTable
{
    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public string Slug { get; set; } = null!;

    public int Order { get; set; }

    public int? ClientId { get; set; }

    #region Navigation Properties

    public virtual TbClient? Client { get; set; }

    public virtual ICollection<TbProjectImage> ProjectImages { get; set; } = new List<TbProjectImage>();

    #endregion

}