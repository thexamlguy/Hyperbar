using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public interface IProxyServiceCollection<T>
{
    IServiceCollection Services { get; }
}