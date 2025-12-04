namespace Abyat.Domains.Models;

public class TbServiceFeature : BaseTable
{
    public Guid ServiceId { get; set; }
    public Guid FeatureId { get; set; }
    public virtual TbService Service { get; set; } = null!;
    public virtual TbFeature Feature { get; set; } = null!;

}