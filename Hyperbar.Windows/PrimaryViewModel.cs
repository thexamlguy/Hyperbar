using CommunityToolkit.Mvvm.ComponentModel;

namespace Hyperbar.Widget;

[NotificationHandler(nameof(IWidgetHostViewModel))]
public partial class PrimaryViewModel(ITemplateFactory templateFactory,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    int index) :
    ObservableCollectionViewModel<IWidgetViewModel>(serviceFactory, mediator, disposer),
    IWidgetHostViewModel,
    ITemplatedViewModel
{
    [ObservableProperty]
    private int index = index;

    public ITemplateFactory TemplateFactory => templateFactory;
}