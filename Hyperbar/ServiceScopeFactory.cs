﻿using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public class ServiceScopeFactory<TService>(IServiceScopeFactory serviceScopeFactory, 
    ICache<TService, IServiceScope> cache) :
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
                    cache.Add(service, serviceScope);
                    return service;
                }
            }
        }

        return default;
    }
}