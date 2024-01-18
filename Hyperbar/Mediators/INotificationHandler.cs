namespace Hyperbar;

public interface INotificationHandler<in TNotification> :
    IHandler
    where TNotification :
    INotification
{
    Task Handle(TNotification notification,
        CancellationToken cancellationToken);
}