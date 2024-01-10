
namespace Hyperbar;

public class WidgetContainerViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IEnumerable<IWidgetViewModel> items) :
    ObservableCollectionViewModel<IWidgetViewModel>(serviceFactory, mediator, items),
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}