using System.Collections.ObjectModel;

namespace Hyperbar;

public class ObservableCollectionViewModel<TItem> :
    ObservableCollection<TItem>
{
    private readonly IServiceFactory serviceFactory;

    public ObservableCollectionViewModel(IServiceFactory serviceFactory)
    {
        this.serviceFactory = serviceFactory;
    }

    public ObservableCollectionViewModel(IServiceFactory serviceFactory, 
        IEnumerable<TItem> items)
    {
        this.serviceFactory = serviceFactory;
        AddRange(items);
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
        Add(item);

        return item;
    }

    public TItem Add<T>()
        where T :
        TItem
    {
        T? item = serviceFactory.Create<T>();
        Add(item);

        return item;
    }

    public void AddRange(IEnumerable<TItem> items)
    {
        foreach (TItem? item in items)
        {
            Add(item);
        }
    }
}

public class ObservableCollectionViewModel(IServiceFactory serviceFactory) :
    ObservableCollectionViewModel<object>(serviceFactory)
{
}