using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Hyperbar;

public class Mediator(IServiceProvider provider) :
    IMediator
{
    private readonly ConditionalWeakTable<Type, dynamic> addedHandlers = [];

    public ValueTask PublishAsync<TNotification>(TNotification notification,
        CancellationToken cancellationToken = default)
        where TNotification :
        INotification
    {
        List<INotificationHandler<TNotification>> handlers =
            provider.GetServices<INotificationHandler<TNotification>>().ToList();

        foreach (KeyValuePair<Type, dynamic> handler in addedHandlers)
        {
            if (handler.Key == typeof(TNotification))
            {
                handlers.Add(handler.Value);
            }
        }

        foreach (INotificationHandler<TNotification> handler in handlers)
        {
            return  handler.Handle(notification, cancellationToken);
        }

        return default;
    }

    public ValueTask<TResponse> SendAsync<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        dynamic? handler = provider.GetService(typeof(RequestClassHandlerWrapper<,>)
            .MakeGenericType(request.GetType(), typeof(TResponse)));

        if (handler is not null)
        {
            return handler.Handle((dynamic)request, cancellationToken);
        }

        return default;
    }

    public ValueTask<object?> SendAsync(object message,
        CancellationToken cancellationToken = default)
    {
        if (message.GetType().GetInterface(typeof(IRequest<>).Name) is { } requestType)
        {
            if (requestType.GetGenericArguments() is { Length: 1 } arguments)
            {
                Type responseType = arguments[0];

                dynamic? handler = provider.GetService(typeof(RequestClassHandlerWrapper<,>)
                    .MakeGenericType(message.GetType(), responseType));

                if (handler is not null)
                {
                    return handler.Handle((dynamic)message, cancellationToken);
                }
            }
        }

        return default;
    }

    public void Subscribe(object subject)
    {
        Type[] interfaceTypes = subject.GetType().GetInterfaces();
        foreach (Type interfaceType in interfaceTypes.Where(x => x.IsGenericType))
        {
            if (interfaceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>))
            {
                if (interfaceType.GetGenericArguments() is { Length: 1 } arguments)
                {
                    Type notificationType = arguments[0];
                    addedHandlers.Add(notificationType, subject);
                }
            }
        }
    }
}