namespace Abyat.Domains.Models;

public class TbProcess : BaseTable
{
    public string Title { get; set; } = null!;

    public virtual ICollection<TbProcessStep> ProcessSteps { get; set; } = new List<TbProcessStep>();

    public virtual ICollection<TbService> Services { get; set; } = new List<TbService>();

}