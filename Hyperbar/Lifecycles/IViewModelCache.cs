namespace Hyperbar;

public interface IViewModelCache<TKey, TViewModel> : 
    IDictionary<TKey, TViewModel> 
    where TKey : notnull
{

}
