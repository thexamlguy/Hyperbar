using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar;

public class ObservableViewModel(IDisposer disposer) : 
    ObservableObject,
    IDisposable
{
    public void Dispose() => disposer.Dispose(this);
}
