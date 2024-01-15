using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Hyperbar.Windows.MediaController;

public class MediaButtonViewModel(IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    ITemplateFactory templateFactory,
    Guid guid = default,
    string? text = null,
    string? icon = null,
    RelayCommand? command = null) :
    WidgetButtonViewModel(serviceFactory, mediator, disposer, templateFactory, guid, text, icon, command),
    IViewModelInitialization
{
    public ICommand Initialize => new AsyncRelayCommand(InitializeAsync);

    public async Task InitializeAsync() => await Mediator.PublishAsync<Request<Playback>>();
}
