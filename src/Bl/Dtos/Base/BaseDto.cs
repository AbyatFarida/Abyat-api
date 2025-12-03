using static Abyat.Core.Enums.Status.Status;

namespace Abyat.Bl.Dtos.Base;

public abstract class BaseDto
{
    public int Id { get; set; } = 0;

    public enCurrentState CurrentState { get; set; } = enCurrentState.Deleted;

    public bool IsSelected { get; set; } = false;
}