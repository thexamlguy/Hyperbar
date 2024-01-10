
namespace Hyperbar;

public partial class WidgetBarViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IEnumerable<WidgetContainerViewModel> items) :
    ObservableCollectionViewModel<WidgetContainerViewModel>(serviceFactory, mediator, items),
    ITemplatedViewModel
{
    public ITemplateFactory TemplateFactory => templateFactory;
}
