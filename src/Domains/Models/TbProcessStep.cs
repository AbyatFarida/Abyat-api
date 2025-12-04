namespace Abyat.Domains.Models;

public class TbProcessStep : BaseTable
{
    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string DescriptionEn { get; set; } = null!;

    public string DescriptionAr { get; set; } = null!;

    public int Order { get; set; }

    public Guid ProcessId { get; set; }

    public virtual TbProcess Process { get; set; } = null!;

}