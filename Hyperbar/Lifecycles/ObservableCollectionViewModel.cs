using System.Collections.ObjectModel;

namespace Hyperbar.Lifecycles;

public class ObservableCollectionViewModel :
    ObservableCollection<object>
{
    public void AddRange(IEnumerable<object> collection)
    {
        foreach (var item in collection)
        {
            Add(item);
        }
    }
}
