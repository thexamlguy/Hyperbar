namespace Hyperbar;

public interface IMappingHandler<TFrom, TTo> :
    IHandler
{
    TTo Handle();
}
