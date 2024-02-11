namespace Hyperbar;

public interface IProxyService<TService>
{
    TService Proxy { get; }
}
