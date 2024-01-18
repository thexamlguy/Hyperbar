namespace Hyperbar;

public interface IMediator
{
    Task PublishAsync<TNotification>(TNotification notification,
        CancellationToken cancellationToken = default)
        where TNotification :
        INotification;

    Task PublishAsync<TNotification>(TNotification notification,
        Func<Func<Task>, Task> marshal,
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