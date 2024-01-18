using Windows.Media.Control;

namespace Hyperbar.Windows.MediaController;

public class MediaController : 
    INotificationHandler<Play>,
    INotificationHandler<Pause>,
    INotificationHandler<Request<Playback>>,
    INotificationHandler<Request<MediaInformation>>,
    IDisposable
{
    private readonly IMediator mediator;
    private readonly IDisposer disposer;
    private readonly GlobalSystemMediaTransportControlsSession session;
    private readonly AsyncLock asyncLock = new();

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
        disposer.Dispose(this);
    }

    public async Task Handle(Play notification, 
        CancellationToken cancellationToken) =>
        await session.TryPlayAsync();

    public async Task Handle(Pause notification, 
        CancellationToken cancellationToken) =>
        await session.TryPauseAsync();

    public async Task Handle(Request<Playback> notification, 
        CancellationToken cancellationToken)
    {
        await mediator.PublishAsync(new Changed<Playback>(), cancellationToken);
    }

    public async Task Handle(Request<MediaInformation> _,
        CancellationToken cancellationToken)
    {
        using (await asyncLock)
        {
            try
            {
                GlobalSystemMediaTransportControlsSessionMediaProperties mediaProperties = await session.TryGetMediaPropertiesAsync();
                await mediator.PublishAsync(new Changed<MediaInformation>(new MediaInformation(mediaProperties.Title,
                    mediaProperties.Subtitle)), cancellationToken);
            }
            catch
            {

            }
        }
    }

    private async void OnMediaPropertiesChanged(GlobalSystemMediaTransportControlsSession sender,
        MediaPropertiesChangedEventArgs args)
    {
        using (await asyncLock)
        {
            try
            {
                GlobalSystemMediaTransportControlsSessionMediaProperties mediaProperties = await session.TryGetMediaPropertiesAsync();
                await mediator.PublishAsync(new Changed<MediaInformation>(new MediaInformation(mediaProperties.Title,
                    mediaProperties.Artist)));
            }
            catch
            {

            }
        }
    }

    private async void OnPlaybackInfoChanged(GlobalSystemMediaTransportControlsSession sender,
        PlaybackInfoChangedEventArgs args)
    {
        await mediator.PublishAsync(new Changed<Playback>());
    }
}