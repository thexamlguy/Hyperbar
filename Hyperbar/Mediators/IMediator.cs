namespace Hyperbar;

public interface IMediator
{
    ValueTask PublishAsync<TNotification>(TNotification notification,
        CancellationToken cancellationToken = default)
        where TNotification :
        INotification;

    ValueTask<TResponse> SendAsync<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = default);

    ValueTask<object?> SendAsync(object message, CancellationToken
        cancellationToken = default);

    void Subscribe(object subscriber);
}