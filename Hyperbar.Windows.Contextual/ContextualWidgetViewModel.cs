
namespace Hyperbar.Widget.Contextual;

public class ContextualWidgetViewModel :
    ObservableCollectionViewModel<IWidgetComponentViewModel>,
    IWidgetViewModel,
    ITemplatedViewModel
{
    public ContextualWidgetViewModel(ITemplateFactory templateFactory,
        IServiceFactory serviceFactory, 
        IMediator mediator, 
        IEnumerable<IWidgetComponentViewModel> items) : base(serviceFactory, mediator, items)
    {
        TemplateFactory = templateFactory;
    }

    public ITemplateFactory TemplateFactory { get; }
}