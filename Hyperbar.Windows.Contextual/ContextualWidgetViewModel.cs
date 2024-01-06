namespace Hyperbar.Widget.Contextual;

public class ContextualWidgetViewModel :
    ObservableCollectionViewModel<IWidgetComponentViewModel>,
    IWidgetViewModel,
    ITemplatedViewModel
{
    public ContextualWidgetViewModel(ITemplateFactory templateFactory,
        IServiceFactory serviceFactory,
        IEnumerable<IWidgetComponentViewModel> items) : base(serviceFactory, items)
    {
        TemplateFactory = templateFactory;
    }

    public ITemplateFactory TemplateFactory { get; }
}