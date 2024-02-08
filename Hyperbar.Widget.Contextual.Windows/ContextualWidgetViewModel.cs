namespace Hyperbar.Widget.Contextual.Windows;

public class ContextualWidgetViewModel(IViewModelTemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    IEnumerable<IWidgetComponentViewModel> items) :
    ObservableCollectionViewModel<IWidgetComponentViewModel>(serviceFactory, mediator, disposer, items),
    IWidgetViewModel
{
    public IViewModelTemplateFactory TemplateFactory => templateFactory;
}