namespace Hyperbar;

public interface IViewModelFactory<TParameter, TViewModel>
{
    ValueTask<TViewModel> CreateAsync(TParameter value);
}

public interface IViewModelCache<TKey, TViewModel>
{
    void Add(TKey key, TViewModel value);

    void Remove(TKey key);
}

public class ViewModelCache<TKey, TViewModel> :
    IViewModelCache<TKey, TViewModel>
{
    private readonly Dictionary<TKey, TViewModel> cache = [];

    public void Add(TKey key, 
        TViewModel value)
    {
        cache.TryAdd(key, value);
    }

    public void Remove(TKey key)
    {
        cache.Remove(key);
    }
}