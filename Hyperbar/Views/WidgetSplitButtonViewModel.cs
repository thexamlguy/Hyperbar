using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hyperbar;

[NotificationHandler(nameof(Id))]
public partial class WidgetSplitButtonViewModel(IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    ITemplateFactory templateFactory,
    IEnumerable<IWidgetComponentViewModel> items,
    Guid id = default,
    string? text = null,
    string? icon = null,
    RelayCommand? command = null) : WidgetComponentViewModel(serviceFactory, mediator, disposer, templateFactory, items)
{
    [ObservableProperty]
    private IRelayCommand? click = command;

    [ObservableProperty]
    private string? icon = icon;

    [ObservableProperty]
    private Guid id = id;

    [ObservableProperty]
    private string? text = text;
}