namespace Hyperbar.Widget.Contextual.Windows;

public class ContextualWidgetViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    IEnumerable<IWidgetComponentViewModel> items) :
    ObservableCollectionViewModel<IWidgetComponentViewModel>(serviceFactory, mediator, disposer, items),
    IWidgetViewModel,
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}