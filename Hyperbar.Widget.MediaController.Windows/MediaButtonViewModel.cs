using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Widget.MediaController.Windows;

public class MediaButtonViewModel(IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    ITemplateFactory templateFactory,
    PlaybackButtonType buttonType,
    Guid guid = default,
    string? text = null,
    string? icon = null,
    RelayCommand? command = null) :
    WidgetButtonViewModel(serviceFactory, mediator, disposer, templateFactory, guid, text, icon, command),
    IInitialization, 
    INotificationHandler<Changed<PlaybackInformation>>
{
    public Task Handle(Changed<PlaybackInformation> notification, 
        CancellationToken cancellationToken)
    {
        if (notification.Value is PlaybackInformation information)
        {
            switch (buttonType)
            {
                case PlaybackButtonType.Play:
                    Visible = information.Status is PlaybackStatus.Paused;
                break;
                case PlaybackButtonType.Pause:
                    Visible = information.Status is PlaybackStatus.Playing;
                    break;
            }
        }

        return Task.CompletedTask;
    }

    public override async Task InitializeAsync() => 
        await Mediator.PublishAsync<Request<PlaybackInformation>>();
}
