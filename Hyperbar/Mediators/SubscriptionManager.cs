using System.Collections.Concurrent;
using System.Reflection;

namespace Hyperbar;

public class SubscriptionManager :
    ISubscriptionManager
{
    private readonly ConcurrentDictionary<object, List<WeakReference>> subscriptions = new();

    public IEnumerable<object?> GetHandlers(Type notificationType, object key)
    {
        if (subscriptions.TryGetValue($"{key?.ToString()}:{notificationType}", 
            out List<WeakReference>? subscribers))
        {
            foreach (WeakReference weakRef in subscribers.ToArray())
            {
                object? target = weakRef.Target;
                if (target != null)
                {
                    yield return target;
                }
                else
                {
                    subscribers.Remove(weakRef);
                }
            }
        }
    }

    public void Remove(object subscriber)
    {
        Type handlerType = subscriber.GetType();
        object? key = GetKeyFromHandler(handlerType, subscriber);
        foreach (Type interfaceType in GetHandlerInterfaces(handlerType))
        {
            if (interfaceType.GetGenericArguments().FirstOrDefault() is Type argumentType)
            {
                if (subscriptions.TryGetValue($"{(key is not null ? $"{key}:" : "")}{argumentType}", out List<WeakReference>? subscribers))
                {
                    for (int i = subscribers.Count - 1; i >= 0; i--)
                    {
                        if (!subscribers[i].IsAlive || subscribers[i].Target == subscriber)
                        {
                            subscribers.RemoveAt(i);
                        }
                    }
                }
            }
        }
    }

    public void Add(object subscriber)
    {
        Type handlerType = subscriber.GetType();
        object? key = GetKeyFromHandler(handlerType, subscriber);
        foreach (Type interfaceType in GetHandlerInterfaces(handlerType))
        {
            if (interfaceType.GetGenericArguments().FirstOrDefault() is Type argumentType)
            {
                subscriptions.AddOrUpdate($"{(key is not null ? $"{key}:" : "")}{argumentType}", _ => new List<WeakReference> { new WeakReference(subscriber) }, (_, collection) =>
                {
                    collection.Add(new WeakReference(subscriber));
                    return collection;
                });
            }
        }
    }

    private static object? GetKeyFromHandler(Type handlerType, object handler)
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

    private static IEnumerable<Type> GetHandlerInterfaces(Type handlerType) => 
        handlerType.GetInterfaces().Where(interfaceType => interfaceType.IsGenericType &&
        interfaceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>));
}
