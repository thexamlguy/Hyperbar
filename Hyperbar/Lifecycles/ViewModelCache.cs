using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;

namespace Hyperbar;

public class ViewModelCache<TKey, TViewModel>(IDisposer disposer) :
    IViewModelCache<TKey, TViewModel>
    where TKey : notnull
{
    private readonly IDictionary<TKey, TViewModel> cache = new Dictionary<TKey, TViewModel>();

    public TViewModel this[TKey key] 
    {
        get => cache[key]; 
        set => cache[key] = value; 
    }

    public ICollection<TKey> Keys => cache.Keys;

    public ICollection<TViewModel> Values => cache.Values;

    public int Count => cache.Count;

    public bool IsReadOnly => false;

    public void Add(TKey key, TViewModel value)
    {
        disposer.Add(value!, Disposable.Create(() =>
        {
            Remove(key);
        }));
        
        cache.Add(key, value);
    }

    public void Add(KeyValuePair<TKey, TViewModel> item)
    {
        cache.Add(item);
    }

    public void Clear() => cache.Clear();

    public bool Contains(KeyValuePair<TKey, TViewModel> item)
    {
        return cache.Contains(item);
    }

    public bool ContainsKey(TKey key)
    {
        return cache.ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<TKey, TViewModel>[] array, int arrayIndex)
    {
         cache.CopyTo(array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<TKey, TViewModel>> GetEnumerator()
    {
        return cache.GetEnumerator();
    }

    public bool Remove(TKey key)
    {
        return cache.Remove(key);
    }

    public bool Remove(KeyValuePair<TKey, TViewModel> item)
    {
        return cache.Remove(item); 
    }

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TViewModel value)
    {
        return cache.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return cache.GetEnumerator();
    }
}