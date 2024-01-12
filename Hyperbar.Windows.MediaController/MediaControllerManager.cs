using System.Collections.Concurrent;
using System.Threading.Channels;
using Windows.Media.Control;

namespace Hyperbar.Windows.Primary;

public class MediaControllerManager :
    IInitializer
{
    private readonly ConcurrentDictionary<GlobalSystemMediaTransportControlsSession, MediaController> cachedSessions = [];
    private readonly IMediator mediator;
    private readonly Queue<MediaController> mediaControllers;
    private readonly IServiceFactory serviceFactory;

    public MediaControllerManager(IServiceFactory serviceFactory, 
        IMediator mediator,
        Queue<MediaController> mediaControllers)
    {
        this.serviceFactory = serviceFactory;
        this.mediator = mediator;
        this.mediaControllers = mediaControllers;
    }

    private Channel<MediaController> d;
    public async Task InitializeAsync()
    {
        d = Channel.CreateUnbounded<MediaController>();

        _ = Task.Run(async () => {

            await foreach (var coordinates in d.Reader.ReadAllAsync())
            {
                Console.WriteLine(coordinates);
            }
        });


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
        if (serviceFactory.Create<MediaController>(session) is MediaController mediaController)
        {
            await d.Writer.WriteAsync(mediaController);

            mediaControllers.Enqueue(mediaController);

            cachedSessions.TryAdd(session, mediaController);
            await mediator.PublishAsync(new Created<MediaController>(mediaController));
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

    private void RemoveSession(GlobalSystemMediaTransportControlsSession session)
    {
        if (serviceFactory.Create<MediaController>(session) is MediaController mediaController)
        {
            cachedSessions.TryAdd(session, mediaController);

        }
    }
}
