namespace Hyperbar.Widget;

[NotificationHandler(nameof(WidgetBarViewModel))]
public partial class WidgetBarViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer) :
    ObservableCollectionViewModel<WidgetContainerViewModel>(serviceFactory, mediator, disposer),
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}
