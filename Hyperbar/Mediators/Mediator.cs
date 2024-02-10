namespace Hyperbar;

public class Mediator(IServiceProvider provider) :
    IMediator
{
    public Task<TResponse?> SendAsync<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        dynamic? handler = provider.GetService(typeof(HandlerWrapper<,>)
            .MakeGenericType(request.GetType(), typeof(TResponse)));

        if (handler is not null)
        {
            return handler.Handle((dynamic)request, cancellationToken);
        }

        return Task.FromResult<TResponse?>(default);
    }

    public Task<object?> SendAsync(object message,
        CancellationToken cancellationToken = default)
    {
        if (message.GetType().GetInterface(typeof(IRequest<>).Name) is { } requestType)
        {
            if (requestType.GetGenericArguments() is { Length: 1 } arguments)
            {
                Type responseType = arguments[0];

                dynamic? handler = provider.GetService(typeof(HandlerWrapper<,>)
                    .MakeGenericType(message.GetType(), responseType));

                if (handler is not null)
                {
                    return handler.Handle((dynamic)message, cancellationToken);
                }
            }
        }

        return Task.FromResult<object?>(default);
    }
}