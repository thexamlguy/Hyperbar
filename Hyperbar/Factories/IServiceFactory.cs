namespace Hyperbar;

public interface IServiceFactory
{
    TService Create<TService>(params object?[] parameters);
}