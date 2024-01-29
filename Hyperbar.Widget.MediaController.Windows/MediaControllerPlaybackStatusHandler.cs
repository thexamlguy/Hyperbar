using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Widget.MediaController.Windows;

public class MediaControllerPlaybackStatusHandler(IMediator mediator,
    IServiceFactory factory) :
    INotificationHandler<Changed<MediaControllerPlaybackStatus>>
{
    public async Task Handle(Changed<MediaControllerPlaybackStatus> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Value is MediaControllerPlaybackStatus mediaControllerPlaybackInformation)
        {
            if (mediaControllerPlaybackInformation.Status is PlaybackStatus.Playing)
            {
                await mediator.PublishAsync(new Replaced<WidgetComponentViewModel>(2, factory.Create<MediaButtonViewModel>(
                    PlaybackButtonType.Pause, "Pause", "\uE769",
                        new RelayCommand(async () => await mediator.PublishAsync<Pause>()))), nameof(MediaControllerViewModel),
                            cancellationToken);
            }

            if (mediaControllerPlaybackInformation.Status is PlaybackStatus.Paused)
            {
                await mediator.PublishAsync(new Replaced<WidgetComponentViewModel>(2, factory.Create<MediaButtonViewModel>(
                    PlaybackButtonType.Pause, "Play", "\uE768",
                        new RelayCommand(async () => await mediator.PublishAsync<Play>()))), nameof(MediaControllerViewModel), 
                            cancellationToken);
            }
        }
    }
}
