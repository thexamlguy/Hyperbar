namespace Hyperbar;

public interface IPublisher
{
    Task PublishAsync<TNotification>(TNotification notification,
        object key,
        CancellationToken cancellationToken = default)
        where TNotification :
        INotification;

    Task PublishAsync<TNotification>(object key,
        CancellationToken cancellationToken = default)
        where TNotification :
        INotification,
        new();

    Task PublishAsync<TNotification>(TNotification notification,
        CancellationToken cancellationToken = default)
        where TNotification :
        INotification;

    Task PublishAsync(object notification,
        CancellationToken cancellationToken = default);

    Task PublishAsync(object notification,
        Func<Func<Task>, Task> marshal,
        object? key = null,
        CancellationToken cancellationToken = default);

    Task PublishAsync<TNotification>(CancellationToken cancellationToken = default)
        where TNotification :
        INotification,
        new();
}
