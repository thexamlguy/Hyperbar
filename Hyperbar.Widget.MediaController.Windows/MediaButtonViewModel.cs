using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hyperbar.Widget.MediaController.Windows;

[NotificationHandler(nameof(PlaybackButtonType))]
public partial class MediaButtonViewModel(IServiceFactory serviceFactory,
    IMediator mediator,
    IDisposer disposer,
    ITemplateFactory templateFactory,
    PlaybackButtonType playbackButtonType,
    Guid guid = default,
    string? text = null,
    string? icon = null,
    RelayCommand? command = null) :
    WidgetButtonViewModel(serviceFactory, mediator, disposer, templateFactory, guid, text, icon, command)
{
    [ObservableProperty]
    private PlaybackButtonType playbackButtonType = playbackButtonType;

    public Task Handle(Changed<MediaControllerPlaybackStatus> notification, 
        CancellationToken cancellationToken)
    {
        if (notification.Value is MediaControllerPlaybackStatus information)
        {
            //switch (buttonType)
            //{
            //    case PlaybackButtonType.Play:
            //        Visible = information.Status is PlaybackStatus.Paused;
            //    break;
            //    case PlaybackButtonType.Pause:
            //        Visible = information.Status is PlaybackStatus.Playing;
            //        break;
            //}
        }

        return Task.CompletedTask;
    }

    //public override Task OnInitializeAsync()
    //{
    //    await Mediator.PublishAsync<Request<PlaybackInformation>>();
    //}  
}
