using System.Collections;
using System.Collections.Specialized;

namespace Hyperbar;

public interface IObservableCollectionViewModel<TItem> : 
    IList<TItem>,
    IList,
    IReadOnlyList<TItem>,
    INotifyCollectionChanged;