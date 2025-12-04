namespace Abyat.Domains.Models;

public class TbCompany : BaseTable
{
    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public string? AddressEn { get; set; }

    public string? AddressAr { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public Guid? LogoId { get; set; }


    #region Navigation Properties

    public virtual TbImageSize? Logo { get; set; }

    public virtual ICollection<TbClient> Clients { get; set; } = new List<TbClient>();

    #endregion

}