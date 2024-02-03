using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Widget.MediaController.Windows;

public partial class MediaButtonViewModel<TMediaButton>(IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    ITemplateFactory templateFactory,
    IRelayCommand invokeCommand) : 
    WidgetComponentViewModel(serviceFactory, mediator, disposer, templateFactory),
    INotificationHandler<Changed<TMediaButton>>,
    IMediaButtonViewModel
    where TMediaButton :
    MediaButton
{
    [ObservableProperty]
    private IRelayCommand? invokeCommand = invokeCommand;

    [ObservableProperty]
    private bool isEnabled;

    [ObservableProperty]
    private string? state = $"{typeof(TMediaButton).Name}";

    public Task Handle(Changed<TMediaButton> args, 
        CancellationToken cancellationToken)
    {
        IsEnabled = args.Value is not null && args.Value.IsEnabled;
        return Task.CompletedTask;
    }

    public override async Task InitializeAsync() =>
        await Mediator.PublishAsync<Request<TMediaButton>>();
}