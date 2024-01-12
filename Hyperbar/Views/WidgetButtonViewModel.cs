using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hyperbar;

public partial class WidgetButtonViewModel(IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    ITemplateFactory templateFactory,
    Guid id = default,
    string? icon = null,
    RelayCommand? command = null) : WidgetComponentViewModel(serviceFactory, mediator, disposer, templateFactory)
{
    [ObservableProperty]
    private Guid id = id;

    [ObservableProperty]
    private string? icon = icon;

    [ObservableProperty]
    private IRelayCommand? click = command;
}