using static Abyat.Core.Enums.Status.Status;

public abstract class BaseTable
{
    public int Id { get; set; }

    public enCurrentState CurrentState { get; set; } = enCurrentState.Active;

    public DateTimeOffset CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }
}
