namespace Hyperbar;

public interface INotificationHandler<in TNotification> :
    IHandler
    where TNotification :
    INotification
{
    ValueTask Handle(TNotification notification,
        CancellationToken cancellationToken);
}