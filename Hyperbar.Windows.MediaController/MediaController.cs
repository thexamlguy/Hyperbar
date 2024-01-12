using Windows.Media.Control;

namespace Hyperbar.Windows.Primary;

public class MediaController : 
    INotificationHandler<PlayRequest>
{
    private readonly GlobalSystemMediaTransportControlsSession session;

    public MediaController(GlobalSystemMediaTransportControlsSession session, 
        IMediator mediator)
    {
        this.session = session;
        mediator.Subscribe(this);
    }

    public async ValueTask Handle(PlayRequest notification, CancellationToken cancellationToken) => 
        await session.TryPlayAsync();
}