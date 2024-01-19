
using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar;

public partial class WidgetContainerViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    IEnumerable<IWidgetViewModel> items,
    Guid id,
    bool alternate) :
    ObservableCollectionViewModel<IWidgetViewModel>(serviceFactory, mediator, disposer, items),
    ITemplatedViewModel
{
    [ObservableProperty]
    private bool alternate = alternate;

    [ObservableProperty]
    private Guid id = id;

    public ITemplateFactory TemplateFactory => templateFactory;
}