﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Hyperbar;

public partial class ObservableCollectionViewModel<TItem> : 
    ObservableObject,
    IList<TItem>, 
    IList, 
    IReadOnlyList<TItem>, 
    INotifyCollectionChanged,
    INotificationHandler<CollectionChanged<IEnumerable<TItem>>>
{
    public ObservableCollection<TItem> collection = [];
    private readonly SynchronizationContext? context;
    private readonly IServiceFactory serviceFactory;

    public ObservableCollectionViewModel(IServiceFactory serviceFactory,
        IMediator mediator)
    {
        context = SynchronizationContext.Current;

        this.serviceFactory = serviceFactory;
        mediator.Subscribe(this);

        collection.CollectionChanged += OnCollectionChanged;
    }

    public ObservableCollectionViewModel(IServiceFactory serviceFactory,
        IMediator mediator,
        IEnumerable<TItem> items)
    {
        context = SynchronizationContext.Current;

        this.serviceFactory = serviceFactory;
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

    protected IList<TItem> Items => collection;

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
        TItem? item = serviceFactory.Create<TItem>();

        Add(item);
        return item;
    }

    public TItem Add<T>(params object?[] parameters)
        where T : TItem
    {
        T? item = serviceFactory.Create<T>(parameters);
        context?.Post(state => Add(item), null);

        return item;
    }

    public TItem Add<T>()
        where T :
        TItem
    {
        T? item = serviceFactory.Create<T>();
        context?.Post(state => Add(item), null);

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
            context?.Post(state => Add(item), null);
        }
    }
    public void Clear() => ClearItems();

    public bool Contains(TItem item) => collection.Contains(item);

    bool IList.Contains(object? value) => IsCompatibleObject(value) && Contains((TItem)value!);

    public void CopyTo(TItem[] array, int index) => collection.CopyTo(array, index);

    void ICollection.CopyTo(Array array, int index) => collection.CopyTo((TItem[])array, index);

    public IEnumerator<TItem> GetEnumerator() => collection.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)collection).GetEnumerator();

    public ValueTask Handle(CollectionChanged<IEnumerable<TItem>> notification,
        CancellationToken cancellationToken)
    {
        context?.Post(state => Clear(), null);
        AddRange(notification.Items);

        return ValueTask.CompletedTask;
    }

    public int IndexOf(TItem item) => collection.IndexOf(item);

    int IList.IndexOf(object? value) => IsCompatibleObject(value) ? IndexOf((TItem)value!) : -1;

    public void Insert(int index, TItem item) => InsertItem(index, item);

    void IList.Insert(int index, object? value)
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
    public void RemoveAt(int index) => RemoveItem(index);

    protected virtual void ClearItems() => collection.Clear();

    protected virtual void InsertItem(int index, TItem value)
    {
        if (value is TItem item)
        {
            collection.Insert(index, item);
        }
    }

    protected virtual void RemoveItem(int index) => collection.RemoveAt(index);

    protected virtual void SetItem(int index, TItem item) => collection[index] = item;

    private static bool IsCompatibleObject(object? value) => (value is TItem) || (value == null && default(TItem) == null);

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs args) => CollectionChanged?.Invoke(this, args);
}

public class ObservableCollectionViewModel(IServiceFactory serviceFactory, IMediator mediator) :
    ObservableCollectionViewModel<object>(serviceFactory, mediator);