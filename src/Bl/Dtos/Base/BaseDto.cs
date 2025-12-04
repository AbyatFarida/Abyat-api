using static Abyat.Core.Enums.Status.Status;

namespace Abyat.Bl.Dtos.Base;

public abstract class BaseDto
{
    public Guid Id { get; set; } = new Guid();

    public enCurrentState CurrentState { get; set; } = enCurrentState.Deleted;

    public bool IsSelected { get; set; } = false;
}