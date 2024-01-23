
namespace Hyperbar;

public interface IValue<TValue> 
    where TValue :
    notnull, new()
{
    Task SetAsync(Func<TValue, TValue> updateDelgate);
}
