namespace Abyat.Domains.Models;

public class TbImageSize : BaseTable
{
    public Guid? SmallSizeId { get; set; }
    public Guid MediumSizeId { get; set; }
    public Guid? LargeSizeId { get; set; }

    #region Navigation Props

    public virtual TbImage? SmallSize { get; set; } = null!;
    public virtual TbImage MediumSize { get; set; } = null!;
    public virtual TbImage? LargeSize { get; set; } = null!;

    public virtual ICollection<TbSlider> Slider { get; set; } = new List<TbSlider>();
    public virtual ICollection<TbCompany> Companies { get; set; } = new List<TbCompany>();
    public virtual ICollection<TbProductImage> ProductImages { get; set; } = new List<TbProductImage>();
    public virtual ICollection<TbProjectImage> ProjectImage { get; set; } = new List<TbProjectImage>();
    public virtual ICollection<TbServiceImage> ServiceImage { get; set; } = new List<TbServiceImage>();
    public virtual ICollection<TbTestimonialImage> Testimonials { get; set; } = new List<TbTestimonialImage>();

    #endregion

}