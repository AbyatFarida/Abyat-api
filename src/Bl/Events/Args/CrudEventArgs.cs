using static Abyat.Bl.Enums.Events.EntityEnums;

namespace Abyat.Bl.Events.Args;

public class CrudEventArgs : EventArgs
{
    public CrudEventArgs(BaseTable entity, int userId)
    {
        Entity = entity;
        UserId = userId;
    }

    public enCrudOp Operation { get; protected set; } = enCrudOp.NotSpecified;
    public BaseTable Entity { get; protected set; }
    public DateTimeOffset EventTime { get; private set; } = DateTime.UtcNow;
    public int UserId { get; protected set; }

}
