namespace Hyperbar;

public interface IMappingFactory<TFrom, TTo>
{
    TTo Create();
}
