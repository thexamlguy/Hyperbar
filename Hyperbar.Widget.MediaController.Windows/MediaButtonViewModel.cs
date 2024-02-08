using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Widget.MediaController.Windows;

public partial class MediaButtonViewModel<TMediaButton>(IServiceProvider serviceProvider,
    IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    IViewModelTemplateFactory templateFactory,
    IRelayCommand invokeCommand) : 
    WidgetComponentViewModel(serviceProvider, serviceFactory, mediator, disposer, templateFactory),
    INotificationHandler<Changed<MediaButton<TMediaButton>>>,
    IMediaButtonViewModel
{
    [ObservableProperty]
    private IRelayCommand? invokeCommand = invokeCommand;

    [ObservableProperty]
    private string? state;

    [ObservableProperty]
    private string? button = $"{typeof(TMediaButton).Name}";

    public Task Handle(Changed<MediaButton<TMediaButton>> args, 
        CancellationToken cancellationToken)
    {
        State = $"{args.Value?.State}";
        return Task.CompletedTask;
    }

    public override async Task InitializeAsync() =>
        await Mediator.PublishAsync<Request<TMediaButton>>();
}