using static Abyat.Core.Enums.Status.Status;

public abstract class BaseTable
{
    public Guid Id { get; set; }

    public enCurrentState CurrentState { get; set; } = enCurrentState.Active;

    public DateTimeOffset CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }
}
