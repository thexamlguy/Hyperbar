using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar.Widget;

public partial class WidgetComponentViewModel :
    ObservableCollectionViewModel<IWidgetComponentViewModel>,
    IWidgetComponentViewModel,
    ITemplatedViewModel
{
    public WidgetComponentViewModel(IServiceFactory serviceFactory,
        IMediator mediator, 
        IDisposer disposer,
        ITemplateFactory templateFactory,
        IEnumerable<IWidgetComponentViewModel> items) : base(serviceFactory, mediator, disposer, items)
    {
        TemplateFactory = templateFactory;
    }

    public WidgetComponentViewModel(IServiceFactory serviceFactory,
        IMediator mediator,
        IDisposer disposer,
        ITemplateFactory templateFactory) : base(serviceFactory, mediator, disposer)
    {
        TemplateFactory = templateFactory;
    }

    public ITemplateFactory TemplateFactory { get; private set; }
}