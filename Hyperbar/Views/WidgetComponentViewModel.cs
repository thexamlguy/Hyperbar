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

public partial class WidgetComponentViewModel(IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    ITemplateFactory templateFactory) : ObservableViewModel(serviceFactory, mediator, disposer),
    IWidgetComponentViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}