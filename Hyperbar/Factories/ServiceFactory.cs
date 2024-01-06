namespace Hyperbar;

public class ServiceFactory(Func<Type, object?[], object> factory) : 
    IServiceFactory
{
    public TService Create<TService>(params object?[] parameters) => 
        (TService)factory(typeof(TService), parameters);
}
