using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reactive.Disposables;

namespace Hyperbar;

public partial class ObservableCollectionViewModel<TItem> :
    ObservableObject,
    IObservableCollectionViewModel<TItem>,
    INotificationHandler<Removed<TItem>>,
    INotificationHandler<Created<TItem>>,
    INotificationHandler<Inserted<TItem>>,
    IDisposable
    where TItem :
    IDisposable
{
    public ObservableCollection<TItem> collection = [];
    private readonly IViewModelEnumerator<TItem>? enumerator;

    public ObservableCollectionViewModel(IServiceFactory serviceFactory,
        IMediator mediator,
        IDisposer disposer)
    {
        ServiceFactory = serviceFactory;
        Mediator = mediator;
        Disposer = disposer;

        mediator.Subscribe(this);

        collection.CollectionChanged += OnCollectionChanged;
    }

    public ObservableCollectionViewModel(IServiceFactory serviceFactory,
        IMediator mediator,
        IDisposer disposer,
        IViewModelEnumerator<TItem> enumerator)
    {
        ServiceFactory = serviceFactory;
        Mediator = mediator;
        Disposer = disposer;

        this.enumerator = enumerator;

        mediator.Subscribe(this);

        collection.CollectionChanged += OnCollectionChanged;

        if (enumerator is not null)
        {
            foreach (TItem? item in enumerator.Next())
            {
                if (item is not null)
                {
                    Add(item);
                }
            }
        }
    }

    public ObservableCollectionViewModel(IServiceFactory serviceFactory,
        IMediator mediator,
        IDisposer disposer,
        IEnumerable<TItem> items)
    {
        ServiceFactory = serviceFactory;
        Mediator = mediator;
        Disposer = disposer;

        mediator.Subscribe(this);

        collection.CollectionChanged += OnCollectionChanged;

        AddRange(items);
    }

    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public int Count => collection.Count;

    bool IList.IsFixedSize => false;

    bool ICollection<TItem>.IsReadOnly => false;

    bool IList.IsReadOnly => false;

    bool ICollection.IsSynchronized => false;

    object ICollection.SyncRoot => this;

    protected IDisposer Disposer { get; private set; }

    protected IList<TItem> Items => collection;

    protected IMediator Mediator { get; private set; }

    protected IServiceFactory ServiceFactory { get; private set; }

    public TItem this[int index]
    {
        get => collection[index];
        set
        {
            SetItem(index, value);
        }
    }

    object? IList.this[int index]
    {
        get => collection[index];
        set
        {
            TItem? item = default;

            try
            {
                item = (TItem)value!;
            }
            catch (InvalidCastException)
            {

            }

            this[index] = item!;
        }
    }

    public TItem Add()
    {
        TItem? item = ServiceFactory.Create<TItem>();

        Add(item);
        return item;
    }

    public TItem Add<T>(params object?[] parameters)
        where T : TItem
    {
        T? item = ServiceFactory.Create<T>(parameters);
        Add(item);

        return item;
    }

    public TItem Add<T>()
        where T :
        TItem
    {
        T? item = ServiceFactory.Create<T>();
        Add(item);

        return item;
    }

    public void Add(TItem item)
    {
        int index = collection.Count;
        InsertItem(index, item);
    }

    int IList.Add(object? value)
    {
        TItem? item = default;

        try
        {
            item = (TItem)value!;
        }
        catch (InvalidCastException)
        {

        }

        Add(item!);
        return Count - 1;
    }

    public void AddRange(IEnumerable<TItem> items)
    {
        foreach (TItem? item in items)
        {
            Add(item);
        }
    }

    public void Clear() => ClearItems();

    public bool Contains(TItem item) => 
        collection.Contains(item);

    bool IList.Contains(object? value) =>
        IsCompatibleObject(value) && Contains((TItem)value!);

    public void CopyTo(TItem[] array, int index) => 
        collection.CopyTo(array, index);

    void ICollection.CopyTo(Array array, int index) =>
        collection.CopyTo((TItem[])array, index);

    public void Dispose() => Disposer.Dispose(this);

    public IEnumerator<TItem> GetEnumerator() =>
        collection.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        ((IEnumerable)collection).GetEnumerator();

    public ValueTask Handle(Removed<TItem> notification,
        CancellationToken cancellationToken)
    {
        foreach (TItem item in this.ToList())
        {
            if (notification.Value is not null)
            {
                if (notification.Value.Equals(item))
                {
                    if (item is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        return ValueTask.CompletedTask;
    }

    public ValueTask Handle(Created<TItem> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Target.Equals(GetType().Name))
        {
            if (notification.Value is TItem item)
            {
                Add(item);
            }
        }

        return ValueTask.CompletedTask;
    }

    public ValueTask Handle(Inserted<TItem> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Target.Equals(GetType().Name))
        {
            if (notification.Value is TItem item)
            {
                Insert(notification.Index, item);
            }
        }

        return ValueTask.CompletedTask;
    }

    public int IndexOf(TItem item) =>
            collection.IndexOf(item);

    int IList.IndexOf(object? value) => 
        IsCompatibleObject(value) ? 
        IndexOf((TItem)value!) : -1;

    public void Insert(int index, TItem item) => 
        InsertItem(index, item);

    void IList.Insert(int index,
        object? value)
    {
        if (value is TItem item)
        {
            Insert(index, item);
        }
    }

    public bool Remove(TItem item)
    {
        int index = collection.IndexOf(item);
        if (index < 0) return false;

        Disposer.Remove(this, item);
        RemoveItem(index);

        return true;
    }

    void IList.Remove(object? value)
    {
        if (IsCompatibleObject(value))
        {
            Remove((TItem)value!);
        }
    }

    public void RemoveAt(int index) =>
        RemoveItem(index);

    protected virtual void ClearItems() => 
        collection.Clear();

    protected virtual void InsertItem(int index,
        TItem value)
    {
        Disposer.Add(this, Disposable.Create(() =>
        {
            Remove(value);
        }));

        Disposer.Add(value, Disposable.Create(() =>
        {
            Remove(value);
        }));

        collection.Insert(index, value);
    }

    protected virtual void RemoveItem(int index) => 
        collection.RemoveAt(index);

    protected virtual void SetItem(int index, TItem item) => 
        collection[index] = item;

    private static bool IsCompatibleObject(object? value) => 
        (value is TItem) || (value == null && default(TItem) == null);

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs args) => 
        CollectionChanged?.Invoke(this, args);
}

public class ObservableCollectionViewModel(IServiceFactory serviceFactory, 
    IMediator mediator,
    IDisposer disposer) :
    ObservableCollectionViewModel<IDisposable>(serviceFactory, mediator, disposer);