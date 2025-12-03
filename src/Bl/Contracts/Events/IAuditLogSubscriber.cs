namespace Abyat.Bl.Contracts.Events;

public interface IAuditLogSubscriber
{
    void Subscribe<TPublisher>(TPublisher publisher) where TPublisher : ICrudPublisher;

    void UnSubscribe<TPublisher>(TPublisher publisher) where TPublisher : ICrudPublisher;

}