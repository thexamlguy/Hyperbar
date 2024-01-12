
using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar;

public partial class WidgetContainerViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    IEnumerable<IWidgetViewModel> items,
    bool alternate) :
    ObservableCollectionViewModel<IWidgetViewModel>(serviceFactory, mediator, disposer, items),
    ITemplatedViewModel
{
    [ObservableProperty]
    private bool alternate = alternate;

    public ITemplateFactory TemplateFactory => templateFactory;
}