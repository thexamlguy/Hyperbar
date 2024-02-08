using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar.Widget;

[NotificationHandler(nameof(IWidgetHostViewModel))]
public partial class PrimaryViewModel(IViewModelTemplateFactory templateFactory,
    IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    int index) :
    ObservableCollectionViewModel<IWidgetViewModel>(serviceProvider, serviceFactory, mediator, disposer),
    IWidgetHostViewModel
{
    [ObservableProperty]
    private int index = index;

    public IViewModelTemplateFactory TemplateFactory => templateFactory;
}