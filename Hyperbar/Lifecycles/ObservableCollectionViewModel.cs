using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reactive.Disposables;
using System.Windows.Input;

namespace Hyperbar;

public partial class ObservableCollectionViewModel<TItem> :
    ObservableObject,
    IObservableCollectionViewModel<TItem>,
    IList<TItem>,
    IList,
    IReadOnlyList<TItem>,
    INotifyCollectionChanged,
    INotificationHandler<Remove<TItem>>,
    INotificationHandler<Create<TItem>>,
    INotificationHandler<Insert<TItem>>,
    INotificationHandler<Move<TItem>>,
    INotificationHandler<Replace<TItem>>,
    IDisposable,
    IInitialization
    where TItem :
    IDisposable
{
    private readonly ObservableCollection<TItem> collection = [];

    private bool isInitialized;

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

    public ICommand InitializeCommand =>
        new AsyncRelayCommand(CoreInitializeAsync);

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
        set => SetItem(index, value);
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

    public void Add(object item)
    {
        int index = collection.Count;
        InsertItem(index, (TItem)item);
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

    public void Clear()
    {
        foreach (TItem item in collection)
        {
            Disposer.Dispose(item);
        }

        ClearItems();
    }

    public bool Contains(TItem item) =>
        collection.Contains(item);

    bool IList.Contains(object? value) =>
        IsCompatibleObject(value) && Contains((TItem)value!);

    public void CopyTo(TItem[] array, int index) =>
        collection.CopyTo(array, index);

    void ICollection.CopyTo(Array array, int index) =>
        collection.CopyTo((TItem[])array, index);

    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
        Disposer.Dispose(this);
    }

    public System.Collections.Generic.IEnumerator<TItem> GetEnumerator() =>
        collection.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        ((IEnumerable)collection).GetEnumerator();

    public Task Handle(Remove<TItem> notification,
        CancellationToken cancellationToken)
    {
        foreach (TItem item in this.ToList())
        {
            if (notification.Value is not null && notification.Value.Equals(item))
            {
                Remove(item);
            }
        }

        return Task.CompletedTask;
    }

    public Task Handle(Create<TItem> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Value is TItem item)
        {
            Add(item);
        }

        return Task.CompletedTask;
    }

    public Task Handle(Insert<TItem> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Value is TItem item)
        {
            Insert(notification.Index, item);
        }

        return Task.CompletedTask;
    }

    public Task Handle(Move<TItem> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Value is TItem item)
        {
            Move(notification.Index, item);
        }

        return Task.CompletedTask;
    }

    public Task Handle(Replace<TItem> notification, 
        CancellationToken cancellationToken)
    {
        if (notification.Value is TItem item)
        {
            Replace(notification.Index, item);
        }

        return Task.CompletedTask;
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

    public bool Move(int index, TItem item)
    {
        int oldIndex = collection.IndexOf(item);
        if (oldIndex < 0)
        {
            return false;
        }

        RemoveItem(oldIndex);
        Insert(index, item);

        return true;
    }

    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public bool Remove(TItem item)
    {
        int index = collection.IndexOf(item);
        if (index < 0)
        {
            return false;
        }

        Disposer.Dispose(item);
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

    public bool Replace(int index, TItem item)
    {
        if (index <= Count - 1)
        {
            RemoveItem(index);
        }
        else
        {
            index = Count;
        }

        Insert(index, item);

        return true;
    }

    protected virtual void ClearItems() =>
        collection.Clear();

    protected virtual void InsertItem(int index,
        TItem value)
    {
        Disposer.Add(this, value);
        Disposer.Add(value, value, Disposable.Create(() =>
        {
            if (value is IList collection)
            {
                collection.Clear();
            }
        }));

        collection.Insert(index, value);
    }

    protected virtual void RemoveItem(int index) =>
        collection.RemoveAt(index);

    protected virtual void SetItem(int index, TItem item) =>
        collection[index] = item;

    private static bool IsCompatibleObject(object? value) =>
        (value is TItem) || (value == null && default(TItem) == null);

    private async Task CoreInitializeAsync()
    {
        if (isInitialized)
        {
            return;
        }

        isInitialized = true;

        await Mediator.PublishAsync<Enumerate<TItem>>();
        await InitializeAsync();
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs args) => 
        CollectionChanged?.Invoke(this, args);
}

public class ObservableCollectionViewModel(IServiceFactory serviceFactory, 
    IMediator mediator,
    IDisposer disposer) :
    ObservableCollectionViewModel<IDisposable>(serviceFactory, mediator, disposer);