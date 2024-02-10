using Windows.Media.Control;
using Windows.Storage.Streams;

namespace Hyperbar.Widget.MediaController.Windows;

public class MediaController :
    INotificationHandler<Request<MediaPrevious>>,
    INotificationHandler<Request<MediaPlayPause>>,
    INotificationHandler<Request<MediaNext>>,
    INotificationHandler<Request<MediaInformation>>,
    INotificationHandler<Request<MediaPreviousButton>>,
    INotificationHandler<Request<MediaPlayPauseButton>>,
    INotificationHandler<Request<MediaNextButton>>,
    IDisposable
{
    private readonly IDisposer disposer;
    private readonly IPublisher publisher;
    private readonly GlobalSystemMediaTransportControlsSession session;

    private bool isNextEnabled;
    private bool isPreviousEnabled;
    private GlobalSystemMediaTransportControlsSessionPlaybackStatus playbackStatus;

    public MediaController(IPublisher publisher,
        ISubscriber subscriber,
        IDisposer disposer,
        GlobalSystemMediaTransportControlsSession session)
    {
        this.publisher = publisher;
        this.disposer = disposer;
        this.session = session;

        disposer.Add(this);
        subscriber.Add(this);

        session.MediaPropertiesChanged += OnMediaPropertiesChanged;
        session.PlaybackInfoChanged += OnPlaybackInfoChanged;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        disposer.Dispose(this);
    }

    public async Task Handle(Request<MediaInformation> args,
        CancellationToken cancellationToken) => await UpdateMediaInformationAsync();

    public async Task Handle(Request<MediaNext> args,
        CancellationToken cancellationToken)
    {
        await session.TrySkipNextAsync();
        await UpdateMediaStateAsync();
    }

    public async Task Handle(Request<MediaPrevious> args, CancellationToken cancellationToken)
    {
        await session.TrySkipPreviousAsync();
        await UpdateMediaStateAsync();
    }

    public async Task Handle(Request<MediaPreviousButton> args,
        CancellationToken cancellationToken) => await UpdateMediaStateAsync();

    public async Task Handle(Request<MediaNextButton> args, 
        CancellationToken cancellationToken) => await UpdateMediaStateAsync();

    public async Task Handle(Request<MediaPlayPause> args, 
        CancellationToken cancellationToken)
    {
        GlobalSystemMediaTransportControlsSessionPlaybackInfo playbackInfo =
            session.GetPlaybackInfo();

        if (playbackInfo.PlaybackStatus is GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing)
        {
            await session.TryPauseAsync();
        }

        if (playbackInfo.PlaybackStatus is GlobalSystemMediaTransportControlsSessionPlaybackStatus.Paused)
        {
            await session.TryPlayAsync();
        }

        await UpdateMediaStateAsync();
    }

    public async Task Handle(Request<MediaPlayPauseButton> args, 
        CancellationToken cancellationToken) => await UpdateMediaStateAsync();

    private async void OnMediaPropertiesChanged(GlobalSystemMediaTransportControlsSession sender,
            MediaPropertiesChangedEventArgs args)
    {
        await UpdateMediaInformationAsync();
        await UpdateMediaStateAsync();
    }

    private async void OnPlaybackInfoChanged(GlobalSystemMediaTransportControlsSession sender,
        PlaybackInfoChangedEventArgs args) => await UpdateMediaStateAsync();

    private async Task UpdateMediaInformationAsync()
    {
        try
        {
            GlobalSystemMediaTransportControlsSessionMediaProperties mediaProperties =
                 await session.TryGetMediaPropertiesAsync();

            byte[]? buffer = null;

            if (mediaProperties.Thumbnail is not null)
            {
                IRandomAccessStreamWithContentType randomAccessStream =
                    await mediaProperties.Thumbnail.OpenReadAsync();

                var stream = randomAccessStream.AsStream();

                using MemoryStream memoryStream = new();
                await stream.CopyToAsync(memoryStream);
                buffer = memoryStream.ToArray();
            }

            await publisher.PublishAsync(new Changed<MediaInformation>(new MediaInformation(mediaProperties.Title,
                mediaProperties.Artist, buffer)));
        }
        catch
        {

        }
    }

    private async Task UpdateMediaStateAsync()
    {
        try
        {
            GlobalSystemMediaTransportControlsSessionPlaybackInfo playbackInfo =
                session.GetPlaybackInfo();

            await publisher.PublishAsync(new Changed<MediaButton<MediaPlayPauseButton>>(new
                 MediaButton<MediaPlayPauseButton>(playbackInfo.PlaybackStatus is 
                    GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing ? 
                        new MediaButtonPlaying() :
                        new MediaButtonPaused())));

            bool isPreviousEnabled = playbackInfo.Controls.IsPreviousEnabled;
            if (this.isPreviousEnabled != isPreviousEnabled)
            {
                await publisher.PublishAsync(new Changed<MediaButton<MediaPreviousButton>>(new
                    MediaButton<MediaPreviousButton>(isPreviousEnabled ? new MediaButtonEnabled() :
                        new MediaButtonDisabled())));

                this.isPreviousEnabled = isPreviousEnabled;
            }

            bool isNextEnabled = playbackInfo.Controls.IsNextEnabled;
            if (this.isNextEnabled != isNextEnabled)
            {
                await publisher.PublishAsync(new Changed<MediaButton<MediaNextButton>>(new
                    MediaButton<MediaNextButton>(isNextEnabled ? new MediaButtonEnabled() : 
                        new MediaButtonDisabled())));

                this.isNextEnabled = isNextEnabled;
            }

        }
        catch
        {

        }
    }
}