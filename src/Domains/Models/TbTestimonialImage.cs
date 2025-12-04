namespace Abyat.Domains.Models;

public class TbTestimonialImage : BaseTable
{
    public Guid TestimonialId { get; set; }

    public Guid ImageSizeId { get; set; }

    #region Navigation Properties

    public virtual TbImageSize ImageSize { get; set; } = null!;

    public virtual TbTestimonial Testimonial { get; set; } = null!;

    #endregion

}