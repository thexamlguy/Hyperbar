
using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar;

public partial class WidgetContainerViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    Guid id) :
    ObservableCollectionViewModel<IWidgetViewModel>(serviceFactory, mediator, disposer),
    ITemplatedViewModel
{
    [ObservableProperty]
    private Guid id = id;

    public ITemplateFactory TemplateFactory => templateFactory;
}