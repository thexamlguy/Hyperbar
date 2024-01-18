namespace Hyperbar;

public interface IFactory<TParameter, TService>
{
    TService? Create(TParameter value);
}


public interface IFactory<TService>
{
    TService? Create();
}


public interface IProvider<TParameter, TService>
{
    TService? Get(TParameter value);
}


public interface IProvider<TService>
{
    TService? Get();
}
