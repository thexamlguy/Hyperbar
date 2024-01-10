using Windows.Media.Control;

namespace Hyperbar.Windows.Primary;

public class MediaController : 
    INotificationHandler<PlayRequest>, 
    IDisposable
{
    public MediaController(GlobalSystemMediaTransportControlsSession session)
    {

    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public ValueTask Handle(PlayRequest notification, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}