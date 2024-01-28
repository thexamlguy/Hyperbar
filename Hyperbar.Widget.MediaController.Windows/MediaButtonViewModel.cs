using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Widget.MediaController.Windows;

public class MediaButtonViewModel :
    WidgetButtonViewModel,
    IInitialization, 
    INotificationHandler<Changed<PlaybackInformation>>
{
    private readonly PlaybackButtonType buttonType;

    public MediaButtonViewModel(IServiceFactory serviceFactory,
        IMediator mediator,
        IDisposer disposer,
        ITemplateFactory templateFactory,
        PlaybackButtonType buttonType,
        Guid guid = default,
        string? text = null,
        string? icon = null,
        RelayCommand? command = null) : base (serviceFactory, mediator, disposer, templateFactory, guid, text, icon, command)
    {
        this.buttonType = buttonType;
        mediator.Subscribe(this);
    }

    public Task Handle(Changed<PlaybackInformation> notification, 
        CancellationToken cancellationToken)
    {
        if (notification.Value is PlaybackInformation information)
        {
            switch (buttonType)
            {
                case PlaybackButtonType.Play:
                    Visible = information.Status is PlaybackStatus.Playing;
                break;
                case PlaybackButtonType.Pause:
                    Visible = information.Status is PlaybackStatus.Paused;
                    break;
            }
        }

        return Task.CompletedTask;
    }

    public override async Task InitializeAsync() => 
        await Mediator.PublishAsync<Request<PlaybackInformation>>();
}
