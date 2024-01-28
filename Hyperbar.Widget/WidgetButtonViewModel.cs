using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Widget;

[NotificationHandler(nameof(Id))]
public partial class WidgetButtonViewModel(IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    ITemplateFactory templateFactory,
    Guid id,
    string? text = null,
    string? icon = null,
    RelayCommand? command = null) : WidgetComponentViewModel(serviceFactory, mediator, disposer, templateFactory)
{
    [ObservableProperty]
    private IRelayCommand? click = command;

    [ObservableProperty]
    private bool enabled;

    [ObservableProperty]
    private string? icon = icon;

    [ObservableProperty]
    private Guid id = id;

    [ObservableProperty]
    private string? text = text;

    [ObservableProperty]
    private bool visible;
}