namespace Hyperbar;

public interface IProvider<TParameter, TService>
{
    TService? Get(TParameter value);
}

public interface IProvider<TService>
{
    TService? Get();
}