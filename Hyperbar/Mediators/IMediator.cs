namespace Hyperbar;

public interface IMediator
{
    ValueTask PublishAsync<TNotification>(TNotification notification,
        CancellationToken cancellationToken = default)
        where TNotification :
        INotification;

    void Send<TResponse>(ICommand<TResponse> command);

    ValueTask<TResponse> SendAsync<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = default);

    ValueTask<TResponse> SendAsync<TResponse>(ICommand<TResponse> command,
        CancellationToken cancellationToken = default);

    ValueTask<TResponse> SendAsync<TResponse>(IQuery<TResponse> query,
        CancellationToken cancellationToken = default);

    ValueTask<object?> SendAsync(object message, CancellationToken
        cancellationToken = default);

    void Subscribe(object subscriber);
}