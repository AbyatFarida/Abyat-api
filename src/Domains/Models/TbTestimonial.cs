namespace Abyat.Domains.Models;

public class TbTestimonial : BaseTable
{
    public string TxtEn { get; set; } = null!;

    public string TxtAr { get; set; } = null!;

    public int Rating { get; set; }

    public Guid ClientId { get; set; }

    #region Navigation Properties

    public virtual TbClient Client { get; set; } = null!;
    public virtual ICollection<TbTestimonialImage> TestimonialImages { get; set; } = new List<TbTestimonialImage>();

    #endregion

}