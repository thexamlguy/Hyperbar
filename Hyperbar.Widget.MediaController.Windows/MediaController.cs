using Windows.Media.Control;

namespace Hyperbar.Widget.MediaController.Windows;

public class MediaController :
    INotificationHandler<Play>,
    INotificationHandler<Pause>,
    INotificationHandler<Request<PlaybackInformation>>,
    INotificationHandler<Request<MediaInformation>>,
    IDisposable
{
    private readonly AsyncLock asyncLock = new();
    private readonly IDisposer disposer;
    private readonly IMediator mediator;
    private readonly GlobalSystemMediaTransportControlsSession session;

    public MediaController(IMediator mediator,
        IDisposer disposer,
        GlobalSystemMediaTransportControlsSession session)
    {
        this.mediator = mediator;
        this.disposer = disposer;
        this.session = session;

        disposer.Add(this);
        mediator.Subscribe(this);

        session.MediaPropertiesChanged += OnMediaPropertiesChanged;
        session.PlaybackInfoChanged += OnPlaybackInfoChanged;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        disposer.Dispose(this);
    }

    public async Task Handle(Play notification,
        CancellationToken cancellationToken)
    {
        await session.TryPlayAsync();
        await UpdateMediaPlaybackPropertiesAsync();
    }

    public async Task Handle(Pause notification,
        CancellationToken cancellationToken)
    {
        await session.TryPauseAsync();
        await UpdateMediaPlaybackPropertiesAsync();
    }

    public async Task Handle(Request<PlaybackInformation> args,
        CancellationToken cancellationToken) => await UpdateMediaPlaybackPropertiesAsync();

    public async Task Handle(Request<MediaInformation> args,
        CancellationToken cancellationToken) => await UpdateMediaPropertiesAsync();

    private async void OnMediaPropertiesChanged(GlobalSystemMediaTransportControlsSession sender,
        MediaPropertiesChangedEventArgs args) => await UpdateMediaPropertiesAsync();

    private async void OnPlaybackInfoChanged(GlobalSystemMediaTransportControlsSession sender,
        PlaybackInfoChangedEventArgs args) => await UpdateMediaPlaybackPropertiesAsync();

    private async Task UpdateMediaPlaybackPropertiesAsync()
    {
        using (await asyncLock)
        {
            try
            {
                GlobalSystemMediaTransportControlsSessionPlaybackInfo playbackInfo = session.GetPlaybackInfo();
                await mediator.PublishAsync(new Changed<PlaybackInformation>(
                    new PlaybackInformation((PlaybackStatus)playbackInfo.PlaybackStatus)));

            }
            catch
            {

            }
        }
    }

    private async Task UpdateMediaPropertiesAsync()
    {
        using (await asyncLock)
        {
            try
            {
                GlobalSystemMediaTransportControlsSessionMediaProperties mediaProperties =
                     await session.TryGetMediaPropertiesAsync();

                await mediator.PublishAsync(new Changed<MediaInformation>(new MediaInformation(mediaProperties.Title,
                    mediaProperties.Artist)));

            }
            catch
            {

            }
        }
    }
}