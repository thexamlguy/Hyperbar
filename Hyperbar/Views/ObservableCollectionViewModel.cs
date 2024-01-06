using System.Collections.ObjectModel;

namespace Hyperbar;

public class ObservableCollectionViewModel<TItem>(IServiceFactory serviceFactory) :
    ObservableCollection<TItem>
{
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
