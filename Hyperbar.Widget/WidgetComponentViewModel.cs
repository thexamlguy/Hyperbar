namespace Hyperbar.Widget;

public partial class WidgetComponentViewModel :
    ObservableCollectionViewModel<IWidgetComponentViewModel>,
    IWidgetComponentViewModel
{
    public WidgetComponentViewModel(IServiceProvider serviceProvider, 
        IServiceFactory serviceFactory,
        IPublisher publisher, 
        ISubscriber subscriber,
        IDisposer disposer,
        IEnumerable<IWidgetComponentViewModel> items) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer, items)
    {

    }

    public WidgetComponentViewModel(IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer) : base(serviceProvider, serviceFactory, publisher, subscriber, disposer)
    {

    }
}