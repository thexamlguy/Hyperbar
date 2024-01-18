namespace Hyperbar;

public interface IFactory<TParameter, TService>
{
    TService? Create(TParameter value);
}


public interface IFactory<TService>
{
    TService? Create();
}
