﻿using Windows.Media.Control;
using Windows.Storage.Streams;

namespace Hyperbar.Widget.MediaController.Windows;

public class MediaController :
    INotificationHandler<Play>,
    INotificationHandler<Pause>,
    INotificationHandler<Request<MediaControllerPlaybackStatus>>,
    INotificationHandler<Request<MediaInformation>>,
    IDisposable
{
    private readonly AsyncLock asyncLock = new();
    private readonly IDisposer disposer;
    private readonly IMediator mediator;
    private readonly GlobalSystemMediaTransportControlsSession session;

    private GlobalSystemMediaTransportControlsSessionPlaybackStatus playbackStatus;

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

    public async Task Handle(Request<MediaControllerPlaybackStatus> args,
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

                if (playbackInfo.PlaybackStatus != playbackStatus)
                {
                    playbackStatus = playbackInfo.PlaybackStatus;
                    await mediator.PublishAsync(new Changed<MediaControllerPlaybackStatus>(
                        new MediaControllerPlaybackStatus((PlaybackStatus)playbackStatus)));

                }
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

                IRandomAccessStreamWithContentType? randomAccessStream = null;
                if (mediaProperties.Thumbnail is not null)
                {
                    randomAccessStream = 
                        await mediaProperties.Thumbnail.OpenReadAsync();
                }

                await mediator.PublishAsync(new Changed<MediaInformation>(new MediaInformation(mediaProperties.Title,
                    mediaProperties.Artist, randomAccessStream is not null ? randomAccessStream.AsStream() : default)));
            }
            catch
            {

            }
        }
    }
}