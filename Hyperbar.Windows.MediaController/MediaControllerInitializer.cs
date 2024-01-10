using Windows.Media.Control;

namespace Hyperbar.Windows.Primary;

public class MediaControllerInitializer :
    IInitializer
{
    private readonly List<GlobalSystemMediaTransportControlsSession> sessions = [];

    public async Task InitializeAsync()
    {
        GlobalSystemMediaTransportControlsSessionManager mediaTransportControlsSessionManager =
            await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();

        mediaTransportControlsSessionManager.SessionsChanged += OnSessionsChanged;
        IReadOnlyList<GlobalSystemMediaTransportControlsSession> sessions = 
            mediaTransportControlsSessionManager.GetSessions();

        foreach (var session in sessions)
        {
            this.sessions.Add(session);
        }
    }

    private void OnSessionsChanged(GlobalSystemMediaTransportControlsSessionManager sender, 
        SessionsChangedEventArgs args)
    {
        IReadOnlyList<GlobalSystemMediaTransportControlsSession> sessions =
            sender.GetSessions();

        foreach (var session in this.sessions.ToList())
        {
            if (!sessions.Contains(session))
            {
                this.sessions.Remove(session);
            }
        }

        foreach (var session in sessions)
        {
            if (!this.sessions.Contains(session))
            {
                this.sessions.Add(session);
            }
        }
    }
}
