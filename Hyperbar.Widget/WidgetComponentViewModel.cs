namespace Hyperbar.Widget;

public partial class WidgetComponentViewModel :
    ObservableCollectionViewModel<IWidgetComponentViewModel>,
    IWidgetComponentViewModel
{
    public WidgetComponentViewModel(IServiceProvider serviceProvider, 
        IServiceFactory serviceFactory,
        IMediator mediator, 
        IDisposer disposer,
        IViewModelTemplateFactory templateFactory,
        IEnumerable<IWidgetComponentViewModel> items) : base(serviceProvider, serviceFactory, mediator, disposer, items)
    {
        TemplateFactory = templateFactory;
    }

    public WidgetComponentViewModel(IServiceProvider serviceProvider,
        IServiceFactory serviceFactory,
        IMediator mediator,
        IDisposer disposer,
        IViewModelTemplateFactory templateFactory) : base(serviceProvider, serviceFactory, mediator, disposer)
    {
        TemplateFactory = templateFactory;
    }

    public IViewModelTemplateFactory TemplateFactory { get; private set; }
}