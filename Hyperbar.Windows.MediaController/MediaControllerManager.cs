using System.Collections.Concurrent;
using Windows.Media.Control;

namespace Hyperbar.Windows.MediaController;
public class MediaControllerManager(IMediator mediator,
    IFactory<GlobalSystemMediaTransportControlsSession, MediaController> factory) :
    IInitializer
{
    private readonly ConcurrentDictionary<GlobalSystemMediaTransportControlsSession, MediaController> cachedSessions = [];

    public async Task InitializeAsync()
    {
        GlobalSystemMediaTransportControlsSessionManager mediaTransportControlsSessionManager =
            await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();
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
            cachedSessions.TryAdd(session, mediaController);
        }
    }

    private async void OnSessionsChanged(GlobalSystemMediaTransportControlsSessionManager sender,
        SessionsChangedEventArgs args)
    {
        IReadOnlyList<GlobalSystemMediaTransportControlsSession> sessions =
            sender.GetSessions();

        foreach (KeyValuePair<GlobalSystemMediaTransportControlsSession, MediaController> session in
            cachedSessions)
        {
            if (!sessions.Contains(session.Key))
            {
                cachedSessions.TryRemove(session);
            }
        }

        foreach (GlobalSystemMediaTransportControlsSession session in sessions)
        {
            await InitializeSessionAsync(session);
        }
    }
}
