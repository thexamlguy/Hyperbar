using Windows.Media.Control;

namespace Hyperbar.Widget.MediaController.Windows;

public class MediaControllerManager(IMediator mediator,
    IFactory<GlobalSystemMediaTransportControlsSession, MediaController> factory,
    IDispatcher dispatcher,
    IDisposer disposer) :
    IInitializer
{
    private readonly AsyncLock asyncLock = new();
    private readonly List<KeyValuePair<GlobalSystemMediaTransportControlsSession, MediaController>> cache = [];
    private GlobalSystemMediaTransportControlsSessionManager? mediaTransportControlsSessionManager;

    public async Task InitializeAsync()
    {
        mediaTransportControlsSessionManager = await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();
        mediaTransportControlsSessionManager.SessionsChanged += OnSessionsChanged;

        IReadOnlyList<GlobalSystemMediaTransportControlsSession> sessions = 
            mediaTransportControlsSessionManager.GetSessions();
        
        foreach (GlobalSystemMediaTransportControlsSession session in sessions)
        {
            await InitializeSessionAsync(session);
        }
    }

    private async Task InitializeSessionAsync(GlobalSystemMediaTransportControlsSession session)
    {
        if (factory.Create(session) is MediaController mediaController)
        {
            await mediator.PublishAsync(new Created<MediaController>(mediaController));
            cache.Add(new KeyValuePair<GlobalSystemMediaTransportControlsSession, MediaController>(session, mediaController));
        }
    }

    private async void OnSessionsChanged(GlobalSystemMediaTransportControlsSessionManager sender,
        SessionsChangedEventArgs args)
    {
        IReadOnlyList<GlobalSystemMediaTransportControlsSession> sessions =
        sender.GetSessions();

        using (await asyncLock)
        {
            foreach (KeyValuePair<GlobalSystemMediaTransportControlsSession, MediaController> session in
                cache.ToList())
            {
                if (!sessions.Any(x => x.SourceAppUserModelId == session.Key.SourceAppUserModelId))
                {
                    await dispatcher.InvokeAsync(() => disposer.Dispose(session.Value));
                    cache.Remove(session);
                }
            }
        }

        using (await asyncLock)
        {
            foreach (GlobalSystemMediaTransportControlsSession session in sessions)
            {
                if (!cache.Any(x => x.Key.SourceAppUserModelId == session.SourceAppUserModelId))
                {
                    await InitializeSessionAsync(session);
                }
            }
        }
    }
}
