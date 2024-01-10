using System.Runtime.CompilerServices;
using System.Reactive.Disposables;
using System.Collections;

namespace Hyperbar;

public class Disposer : 
    IDisposer
{
    private readonly ConditionalWeakTable<object, CompositeDisposable> subjects = [];

    public void Add(object subject,
        params object[] objects)
    {
        CompositeDisposable disposables = subjects.GetOrCreateValue(subject);
        foreach (IDisposable disposable in objects.OfType<IDisposable>())
        {
            disposables.Add(disposable);
        }

        foreach (object notDisposable in objects.Where(x => x is not IDisposable))
        {
            disposables.Add(Disposable.Create(() => FromNotDisposable(notDisposable)));
        }
    }

    private void FromNotDisposable(object target)
    {
        if (target is IEnumerable enumerableTarget)
        {
            foreach (object? item in enumerableTarget)
            {
                FromNotDisposable(item);
            }
        }

        if (target is IDisposable disposableTarget)
        {
            disposableTarget.Dispose();
        }

        if (target is not IDisposable)
        {
            Dispose(target);
        }
    }

    public TDisposable Replace<TDisposable>(object subject, 
        IDisposable disposer, 
        TDisposable replacement)
        where TDisposable : 
        IDisposable
    {
        CompositeDisposable disposables = subjects.GetOrCreateValue(subject);
        if (disposer is not null)
        {
            disposables.Remove(disposer);
        }

        disposables.Add(replacement);
        return replacement;
    }

    public void Remove(object subject, 
        IDisposable disposer)
    {
        CompositeDisposable disposables = subjects.GetOrCreateValue(subject);
        if (disposer is not null)
        {
            disposables.Remove(disposer);
        }
    }

    public void Dispose(object subject)
    {
        if (subjects.TryGetValue(subject, out CompositeDisposable? disposables))
        {
            disposables?.Dispose();
        }
    }
}
