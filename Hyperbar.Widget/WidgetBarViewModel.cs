namespace Hyperbar.Widget;

[NotificationHandler(nameof(WidgetViewModel))]
public partial class WidgetViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer) :
    ObservableCollectionViewModel<IWidgetViewModel>(serviceFactory, mediator, disposer),
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}
