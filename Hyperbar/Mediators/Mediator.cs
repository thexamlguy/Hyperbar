using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Reflection;

namespace Hyperbar;

public class Mediator(IServiceProvider provider, 
    IDispatcher dispatcher) :
    IMediator
{
    private readonly ConcurrentDictionary<object, List<dynamic>> subscriptions = [];

    public Task PublishAsync<TNotification>(object key,
        CancellationToken cancellationToken = default)
        where TNotification :
        INotification,
        new() => PublishAsync(new TNotification(), args => dispatcher.InvokeAsync(async () => await args()),
            key, cancellationToken);

    public Task PublishAsync<TNotification>(TNotification notification,
        CancellationToken cancellationToken = default)
        where TNotification :
        INotification
    {
        return PublishAsync(notification, args => dispatcher.InvokeAsync(async () => await args()), 
            null, cancellationToken);
    }

    public Task PublishAsync<TNotification>(TNotification notification,
        object key,
        CancellationToken cancellationToken = default)
        where TNotification :
        INotification
    {
        return PublishAsync(notification, args => dispatcher.InvokeAsync(async () => await args()),
            key, cancellationToken);
    }

    public  Task PublishAsync<TNotification>(TNotification notification,
        Func<Func<Task>, Task> marshal,
        object? key = null,
        CancellationToken cancellationToken = default)
        where TNotification :
        INotification
    {
        List<INotificationHandler<TNotification>> handlers =
            provider.GetServices<INotificationHandler<TNotification>>().ToList();

        foreach (KeyValuePair<object, List<dynamic>> subscriber in subscriptions)
        {
            if (subscriber.Key.Equals($"{(key is not null ? $"{key}:" : "")}{typeof(TNotification)}"))
            {
                foreach (dynamic handler in subscriber.Value)
                {
                    handlers.Add(handler);
                }
            }
        }

        foreach (INotificationHandler<TNotification> handler in handlers)
        {
            marshal(() => handler.Handle(notification, cancellationToken));
        }

        return Task.CompletedTask;
    }

    public Task PublishAsync<TNotification>(CancellationToken cancellationToken = default)
        where TNotification :
        INotification,
        new() => PublishAsync(new TNotification(), args => dispatcher.InvokeAsync(async () => await args()), 
            null, cancellationToken);

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

    public void Subscribe(object handler)
    {
        Type handlerType = handler.GetType();
        object? key = GetKeyFromHandler(handlerType, handler);

        foreach (Type interfaceType in GetNotificationHandlerInterfaces(handlerType))
        {
            if (interfaceType.GetGenericArguments().FirstOrDefault() is Type argumentType)
            {
                subscriptions.AddOrUpdate($"{(key is not null ? $"{key}:" : "")}{argumentType}", new List<object> { handler }, (value, collection) =>
                {
                    collection.Add(handler);
                    return collection;
                });
            }
        }
    }

    private object? GetKeyFromHandler(Type handlerType, object handler)
    {
        if (handlerType.GetCustomAttribute<NotificationHandlerAttribute>() is NotificationHandlerAttribute attribute)
        {
            if (handlerType.GetProperty($"{attribute.Key}") is PropertyInfo property
                && property.GetValue(handler) is { } value)
            {
                return value;
            }
            else
            {
                return attribute.Key;
            }
        }

        return null;
    }

    private IEnumerable<Type> GetNotificationHandlerInterfaces(Type handlerType) => handlerType.GetInterfaces()
            .Where(interfaceType => interfaceType.IsGenericType && interfaceType
            .GetGenericTypeDefinition() == typeof(INotificationHandler<>));
}