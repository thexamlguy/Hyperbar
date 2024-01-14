using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hyperbar;

public partial class WidgetMenuViewModel(IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    ITemplateFactory templateFactory,
    Guid guid = default,
    string? text = null,
    string? icon = null,
    RelayCommand? command = null) : WidgetComponentViewModel(serviceFactory, mediator, disposer, templateFactory, guid)
{
    [ObservableProperty]
    private IRelayCommand? click = command;

    [ObservableProperty]
    private string? icon = icon;

    [ObservableProperty]
    private string? text = text;
}