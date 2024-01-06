namespace Hyperbar;

public interface MappingFactory<TFrom, TTo>
{
    TTo Create();
}
