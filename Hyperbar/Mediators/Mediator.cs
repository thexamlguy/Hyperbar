﻿using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Hyperbar;

public class Mediator(IServiceProvider provider) :
    IMediator
{
    private readonly ConditionalWeakTable<Type, dynamic> handlers = [];

    public ValueTask PublishAsync<TNotification>(TNotification notification,
        CancellationToken cancellationToken = default)
        where TNotification :
        INotification
    {
        List<INotificationHandler<TNotification>> handlers =
            provider.GetServices<INotificationHandler<TNotification>>().ToList();

        foreach (KeyValuePair<Type, dynamic> handler in this.handlers)
        {
            if (handler.Key == typeof(TNotification))
            {
                handlers.Add(handler.Value);
            }
        }

        if (handlers.Count == 0)
        {
            return default;
        }
        else if (handlers.Count == 1)
        {
            return handlers[0].Handle(notification, cancellationToken);
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

    public ValueTask<TResponse> SendAsync<TResponse>(ICommand<TResponse> command,
        CancellationToken cancellationToken = default)
    {
        dynamic? handler = provider.GetService(typeof(CommandClassHandlerWrapper<,>)
            .MakeGenericType(command.GetType(), typeof(TResponse)));

        if (handler is not null)
        {
            return handler.Handle((dynamic)command, cancellationToken);
        }

        return default;
    }

    public void Send<TResponse>(ICommand<TResponse> command)
    {
        dynamic? handler = provider.GetService(typeof(CommandClassHandlerWrapper<,>)
            .MakeGenericType(command.GetType(), typeof(TResponse)));

        if (handler is not null)
        {
            _ = handler.Handle((dynamic)command, default(CancellationToken));
        }
    }

    public ValueTask<TResponse> SendAsync<TResponse>(IQuery<TResponse> query,
        CancellationToken cancellationToken = default)
    {
        dynamic? handler = provider.GetService(typeof(QueryClassHandlerWrapper<,>)
            .MakeGenericType(query.GetType(), typeof(TResponse)));

        if (handler is not null)
        {
            return handler.Handle((dynamic)query, cancellationToken);
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

        if (message.GetType().GetInterface(typeof(ICommand<>).Name) is { } commandType)
        {
            if (commandType.GetGenericArguments() is { Length: 1 } arguments)
            {
                Type responseType = arguments[0];

                dynamic? handler = provider.GetService(typeof(CommandClassHandlerWrapper<,>)
                    .MakeGenericType(message.GetType(), responseType));

                if (handler is not null)
                {
                    return handler.Handle((dynamic)message, cancellationToken);
                }
            }
        }

        if (message.GetType().GetInterface(typeof(IQuery<>).Name) is { } queryType)
        {
            if (queryType.GetGenericArguments() is { Length: 1 } arguments)
            {
                Type responseType = arguments[0];

                dynamic? handler = provider.GetService(typeof(QueryClassHandlerWrapper<,>)
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
                    handlers.Add(notificationType, subject);
                }
            }
        }
    }
}