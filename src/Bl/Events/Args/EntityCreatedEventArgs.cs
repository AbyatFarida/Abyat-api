using static Abyat.Bl.Enums.Events.EntityEnums;

namespace Abyat.Bl.Events.Args;

public class EntityCreatedEventArgs : CrudEventArgs
{
    public EntityCreatedEventArgs(BaseTable entity, int userId) : base(entity, userId)
    {
        Operation = enCrudOp.Create;
    }
}
