namespace Hyperbar;

public interface ICache<TKey, TViewModel> : 
    IDictionary<TKey, TViewModel> 
    where TKey : notnull
{

}
