using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Widget;

[NotificationHandler(nameof(Id))]
public partial class WidgetMenuViewModel(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IPublisher publisher,
    ISubscriber subscriber,
    IDisposer disposer,
    Guid id = default,
    string? text = null,
    string? icon = null,
    RelayCommand? command = null) : WidgetComponentViewModel(serviceProvider, serviceFactory, publisher, subscriber, disposer)
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