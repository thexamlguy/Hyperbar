using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Hyperbar;

public class ServiceScopeProvider<TService>(ConcurrentDictionary<TService, IServiceScope> services) :
    IServiceScopeProvider<TService>
    where TService : notnull
{
    public bool TryGet(TService service, 
        out IServiceScope? serviceScope)
    {
        if (services.TryGetValue(service, out IServiceScope? value))
        {
            serviceScope = value;
            return true;
        }

        serviceScope = null;
        return false;
    }
}
