using System.Collections.ObjectModel;

namespace Hyperbar.Lifecycles;

public class ObservableCollectionViewModel<TItem> :
    ObservableCollection<TItem>
{
    public void AddRange(IEnumerable<TItem> collection)
    {
        foreach (TItem? item in collection)
        {
            Add(item);
        }
    }
}

public class ObservableCollectionViewModel :
    ObservableCollectionViewModel<object>
{

}
