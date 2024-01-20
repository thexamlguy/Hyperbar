using System.Collections;
using System.Collections.Concurrent;
using System.Reactive.Disposables;

namespace Hyperbar;

public class Cache<TValue>(IDisposer disposer) :
    ICache<TValue>
{
    private readonly List<TValue> cache = [];

    public void Add(TValue value)
    {
        disposer.Add(value!, Disposable.Create(() =>
        {
            Remove(value);
        }));

        cache.Add(value);
    }

    public void Clear() => cache.Clear();

    public System.Collections.Generic.IEnumerator<TValue> GetEnumerator() => cache.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool Remove(TValue value) => cache.Remove(value);
}

public class Cache<TKey, TValue>(IDisposer disposer) :
    ICache<TKey, TValue>
    where TKey :
    notnull
    where TValue :
    notnull
{
    private readonly ConcurrentDictionary<TKey, TValue> cache = new();

    public void Add(TKey key, 
        TValue value)
    {
        cache.TryAdd(key, value);

        disposer.Add(value, Disposable.Create(() =>
        {
            Remove(key);
        }));

        disposer.Add(key, Disposable.Create(() =>
        {
            Remove(key);
        }));
    }

    public void Clear() => cache.Clear();

    public bool ContainsKey(TKey key) => cache.ContainsKey(key);

    public System.Collections.Generic.IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => cache.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool Remove(TKey key) => cache.Remove(key, out _);

    public bool TryGetValue(TKey key, out TValue? value)
    {
        if (cache.TryGetValue(key, out value))
        {
            return true;
        }

        value = default;
        return false;
    }
}