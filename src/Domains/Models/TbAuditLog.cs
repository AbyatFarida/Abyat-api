namespace Abyat.Domains.Models;

public partial class TbAuditLog : BaseTable
{
    public string Action { get; set; } = null!;

    public string? Entity { get; set; }

    public Guid? EntityId { get; set; }

    public string? Details { get; set; }

}
