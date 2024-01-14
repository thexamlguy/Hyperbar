using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;

namespace Hyperbar;

public class Cache<TKey, TService>(IDisposer disposer) :
    ICache<TKey, TService>
    where TKey : notnull
{
    private readonly IDictionary<TKey, TService> cache = new Dictionary<TKey, TService>();

    public TService this[TKey key] 
    {
        get => cache[key]; 
        set => cache[key] = value; 
    }

    public ICollection<TKey> Keys => cache.Keys;

    public ICollection<TService> Values => cache.Values;

    public int Count => cache.Count;

    public bool IsReadOnly => false;

    public void Add(TKey key, TService value)
    {
        disposer.Add(value!, Disposable.Create(() =>
        {
            Remove(key);
        }));
        
        cache.Add(key, value);
    }

    public void Add(KeyValuePair<TKey, TService> item)
    {
        cache.Add(item);
    }

    public void Clear() => cache.Clear();

    public bool Contains(KeyValuePair<TKey, TService> item)
    {
        return cache.Contains(item);
    }

    public bool ContainsKey(TKey key)
    {
        return cache.ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<TKey, TService>[] array, int arrayIndex)
    {
         cache.CopyTo(array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<TKey, TService>> GetEnumerator()
    {
        return cache.GetEnumerator();
    }

    public bool Remove(TKey key)
    {
        return cache.Remove(key);
    }

    public bool Remove(KeyValuePair<TKey, TService> item)
    {
        return cache.Remove(item); 
    }

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TService value)
    {
        return cache.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return cache.GetEnumerator();
    }
}