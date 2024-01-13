using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar;

public class ObservableViewModel(IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer) : 
    ObservableObject,
    IDisposable
{
    public void Dispose() => disposer.Dispose(this);
}
