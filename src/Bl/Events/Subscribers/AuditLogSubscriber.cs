using Abyat.Bl.Contracts.AuditLog;
using Abyat.Bl.Contracts.Events;
using Abyat.Bl.Dtos.AuditLog;
using Abyat.Bl.Events.Args;

namespace Abyat.Bl.Events.Subscribers;

public class AuditLogSubscriber(IAuditLog auditLog) : IAuditLogSubscriber
{
    public void Subscribe<TPublisher>(TPublisher publisher) where TPublisher : ICrudPublisher
    {
        publisher.Created += Handle;
        publisher.Updated += Handle;
        publisher.StatusChanged += Handle;
    }

    public void UnSubscribe<TPublisher>(TPublisher publisher) where TPublisher : ICrudPublisher
    {
        publisher.Created -= Handle;
        publisher.Updated -= Handle;
        publisher.StatusChanged -= Handle;
    }

    private void Handle<TArgs>(object sender, TArgs args) where TArgs : CrudEventArgs
    {
        var entityName = args.Entity.GetType().Name;

        var details = args switch
        {
            EntityCreatedEventArgs e => $"New item created at ID {e.Entity.Id} in entity {entityName}",
            EntityUpdatedEventArgs e => $"Entity {entityName} with ID {e.Entity.Id} changed from {e.OriginalEntity.ToString()} to {e.Entity.ToString()}",
            EntityStatusChangedEventArgs e => $"Entity {entityName} with ID {e.Entity.Id} state changed from {e.PreviousState} to {e.NewState}",
            _ => throw new InvalidOperationException($"Unsupported event type: {args.GetType().Name}")
        };

        AuditLogDto? dto = new AuditLogDto
        {
            Action = args.Operation.ToString(),
            EntityId = args.Entity.Id,
            Entity = entityName,
            Details = details
        };

        if (dto != null)
        {
            auditLog.AddAsync(dto, false).GetAwaiter().GetResult();
        }

    }

}