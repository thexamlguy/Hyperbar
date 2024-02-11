using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public interface IServiceScopeProvider<TService>
{
    bool TryGet(TService service, out IServiceScope? serviceScope);
}
