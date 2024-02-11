namespace Hyperbar;

public interface IServiceScopeFactory<TService>
{
    TService? Create(params object?[] parameters);
}
