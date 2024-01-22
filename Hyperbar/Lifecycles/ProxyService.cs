namespace Hyperbar;

public class ProxyService<TService>(TService proxy) :
    IProxyService<TService>
{
    public TService Proxy { get; private set; } = proxy;
}