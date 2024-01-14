using Windows.Media.Control;

namespace Hyperbar.Windows.MediaController;

public class MediaController : 
    INotificationHandler<Play>,
    INotificationHandler<Pause>
{
    private readonly IMediator mediator;
    private readonly GlobalSystemMediaTransportControlsSession session;

    public MediaController(IMediator mediator,
        GlobalSystemMediaTransportControlsSession session)
    {
        this.mediator = mediator;
        this.session = session;

        mediator.Subscribe(this);

        session.MediaPropertiesChanged += OnMediaPropertiesChanged;
    }

    private void OnMediaPropertiesChanged(GlobalSystemMediaTransportControlsSession sender, 
        MediaPropertiesChangedEventArgs args)
    {
        mediator.PublishAsync(new Changed<Media>());
    }

    public async ValueTask Handle(Play notification, CancellationToken cancellationToken) => 
        await session.TryPlayAsync();

    public async ValueTask Handle(Pause notification, CancellationToken cancellationToken) =>
        await session.TryPauseAsync();
}