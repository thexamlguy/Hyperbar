namespace Hyperbar;

public interface IServiceFactory
{
    object Create(Type type,
        params object?[] parameters);

    TService Create<TService>(params object?[] parameters);
}
