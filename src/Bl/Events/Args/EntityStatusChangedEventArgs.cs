using static Abyat.Core.Enums.Status.Status;
using static Abyat.Bl.Enums.Events.EntityEnums;

namespace Abyat.Bl.Events.Args;

public class EntityStatusChangedEventArgs : CrudEventArgs
{
    public enCurrentState PreviousState { get; set; }
    public enCurrentState NewState { get; set; }

    public EntityStatusChangedEventArgs(BaseTable entity, Guid userId, enCurrentState previousState, enCurrentState newState) : base(entity, userId)
    {
        Operation = enCrudOp.ChangeState;
        PreviousState = previousState;
        NewState = newState;
    }
}
