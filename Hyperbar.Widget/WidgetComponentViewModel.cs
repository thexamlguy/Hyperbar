namespace Hyperbar.Widget;

public partial class WidgetComponentViewModel :
    ObservableCollectionViewModel<IWidgetComponentViewModel>,
    IWidgetComponentViewModel
{
    public WidgetComponentViewModel(IServiceProvider serviceProvider, 
        IServiceFactory serviceFactory,
        IMediator mediator, 
        IDisposer disposer,
        IEnumerable<IWidgetComponentViewModel> items) : base(serviceProvider, serviceFactory, mediator, disposer, items)
    {

    }

    public WidgetComponentViewModel(IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IMediator mediator,
        IDisposer disposer) : base(serviceProvider, serviceFactory, mediator, disposer)
    {

    }
}