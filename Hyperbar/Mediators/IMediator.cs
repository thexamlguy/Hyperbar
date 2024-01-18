namespace Hyperbar;

[AttributeUsage(AttributeTargets.Class)]
public class NotificationHandlerAttribute(object key) : Attribute
{
    public object Key { get; } = key;
}

public interface IMediator
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

    Task PublishAsync<TNotification>(TNotification notification,
        Func<Func<Task>, Task> marshal,
        object? key = null,
        CancellationToken cancellationToken = default)
        where TNotification :
        INotification;

    Task PublishAsync<TNotification>(CancellationToken cancellationToken = default)
        where TNotification :
        INotification,
        new();

    Task<TResponse?> SendAsync<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = default);

    Task<object?> SendAsync(object message, CancellationToken
        cancellationToken = default);
    
    void Subscribe(object handler);
}