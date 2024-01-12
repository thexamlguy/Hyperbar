namespace Hyperbar;

public partial class WidgetBarViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    IEnumerable<WidgetContainerViewModel> items) :
    ObservableCollectionViewModel<WidgetContainerViewModel>(serviceFactory, mediator, disposer, items),
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}
