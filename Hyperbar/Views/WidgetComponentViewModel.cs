using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar;

public partial class WidgetComponentViewModel :
    ObservableCollectionViewModel<IWidgetComponentViewModel>,
    IWidgetComponentViewModel,
    ITemplatedViewModel
{
    [ObservableProperty]
    private Guid id;

    public WidgetComponentViewModel(IServiceFactory serviceFactory,
        IMediator mediator, 
        IDisposer disposer,
        ITemplateFactory templateFactory,
        IEnumerable<IWidgetComponentViewModel> items,
        Guid id = default) : base(serviceFactory, mediator, disposer, items)
    {
        this.id = id;
        TemplateFactory = templateFactory;
    }

    public WidgetComponentViewModel(IServiceFactory serviceFactory,
        IMediator mediator,
        IDisposer disposer,
        ITemplateFactory templateFactory,
        Guid id = default) : base(serviceFactory, mediator, disposer)
    {
        this.id = id;
        TemplateFactory = templateFactory;
    }

    public ITemplateFactory TemplateFactory { get; private set; }
}