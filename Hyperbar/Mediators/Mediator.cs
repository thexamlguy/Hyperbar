using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Hyperbar;

public class Mediator(IServiceProvider provider, 
    IDispatcher dispatcher) :
    IMediator
{
    private readonly ConcurrentDictionary<Type, List<dynamic>> subjects = [];

    public Task PublishAsync<TNotification>(TNotification notification,
        CancellationToken cancellationToken = default)
        where TNotification :
        INotification
    {
        return PublishAsync(notification, args => dispatcher.InvokeAsync(async () => await args()), cancellationToken);
    }

    public Task PublishAsync<TNotification>(TNotification notification,
        Func<Func<Task>, Task> marshal,
        CancellationToken cancellationToken = default)
        where TNotification :
        INotification
    {
        List<INotificationHandler<TNotification>> handlers =
            provider.GetServices<INotificationHandler<TNotification>>().ToList();

        foreach (KeyValuePair<Type, List<dynamic>> handler in subjects)
        {
            if (handler.Key == typeof(TNotification))
            {
                foreach (dynamic value in handler.Value)
                {
                    handlers.Add(value);
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
        new() => PublishAsync(new TNotification(), null, cancellationToken);

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
        Type[] interfaceTypes = handler.GetType().GetInterfaces();
        foreach (Type interfaceType in interfaceTypes.Where(x => x.IsGenericType))
        {
            if (interfaceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>))
            {
                if (interfaceType.GetGenericArguments() is { Length: 1 } arguments)
                {
                    Type notificationType = arguments[0];
                    subjects.AddOrUpdate(notificationType, [handler], (value, collection) =>
                    {
                        collection.Add(handler);
                        return collection;
                    });
                }
            }
        }
    }
}