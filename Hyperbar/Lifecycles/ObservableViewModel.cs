using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar;

public class ObservableViewModel(IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer) : 
    ObservableObject,
    IDisposable
{
    public IServiceFactory ServiceFactory => serviceFactory;

    public IMediator Mediator => mediator;

    public void Dispose() => disposer.Dispose(this);
}
