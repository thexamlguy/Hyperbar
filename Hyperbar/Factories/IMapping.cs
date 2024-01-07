namespace Hyperbar;

public interface IMapping<TFrom, TTo>
{
    TTo Create();
}
