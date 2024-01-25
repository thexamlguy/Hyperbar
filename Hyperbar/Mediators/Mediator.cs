using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Reflection;

namespace Hyperbar;

public class Mediator(IServiceProvider provider, 
    IDispatcher dispatcher) :
    IMediator
{
    private readonly ConcurrentDictionary<object, List<dynamic>> subjects = [];

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

        foreach (KeyValuePair<object, List<dynamic>> handler in subjects)
        {
            if (key is not null && handler.Key.Equals(key) || handler.Key.Equals(typeof(TNotification)))
            {
                handlers.Add(handler.Value[0]);
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
        object? key = null;

        if (Attribute.GetCustomAttribute(handlerType, typeof(NotificationHandlerAttribute))
            is NotificationHandlerAttribute attribute)
        {
            if (handlerType.GetProperty($"{attribute.Key}") is PropertyInfo property)
            {
                if (property.GetValue(handler, null) is { } value)
                {
                    key = value;
                }
            }
            else
            {
                key = attribute.Key;
            }
        }

        foreach (Type interfaceType in handlerType.GetInterfaces().Where(x => x.IsGenericType 
            && x.GetGenericTypeDefinition() == typeof(INotificationHandler<>)))
        {
            if (interfaceType.GetGenericArguments() is { Length: 1 } arguments)
            {
                key ??= arguments[0];
                subjects.AddOrUpdate(key, [handler], (value, collection) =>
                {
                    collection.Add(handler);
                    return collection;
                });
            }
        }
    }
}