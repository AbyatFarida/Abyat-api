namespace Abyat.Domains.Models;

public class TbServiceFeature : BaseTable
{
    public int ServiceId { get; set; }
    public int FeatureId { get; set; }
    public virtual TbService Service { get; set; } = null!;
    public virtual TbFeature Feature { get; set; } = null!;

}