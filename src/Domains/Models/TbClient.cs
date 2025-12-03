namespace Abyat.Domains.Models;

public class TbClient : BaseTable
{
    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public int? CompanyId { get; set; }

    #region Navigation Properties

    public virtual TbCompany? Company { get; set; }

    public virtual ICollection<TbProject> Projects { get; set; } = new List<TbProject>();

    public virtual ICollection<TbTestimonial> Testimonials { get; set; } = new List<TbTestimonial>();

    #endregion

}