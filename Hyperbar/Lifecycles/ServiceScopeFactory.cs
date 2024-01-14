using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Hyperbar;

public class ServiceScopeFactory<TService>(IServiceScopeFactory serviceScopeFactory, 
    ConcurrentDictionary<TService, IServiceScope> services) :
    IServiceScopeFactory<TService>
    where TService : notnull
{
    public TService? Create(params object?[] parameters)
    {
        if (serviceScopeFactory.CreateScope() is IServiceScope serviceScope)
        {
            if (serviceScope.ServiceProvider.GetService<IServiceFactory>() is IServiceFactory serviceFactory)
            {
                if (serviceFactory.Create<TService>(parameters) is TService service)
                {
                    services.TryAdd(service, serviceScope);
                    return service;
                }
            }
        }

        return default;
    }
}