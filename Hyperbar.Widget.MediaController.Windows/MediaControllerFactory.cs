using Windows.Media.Control;

namespace Hyperbar.Widget.MediaController.Windows;

public class MediaControllerFactory(IServiceScopeFactory<MediaController> serviceScopeFactory) :
    IFactory<GlobalSystemMediaTransportControlsSession, MediaController?>
{
    public MediaController? Create(GlobalSystemMediaTransportControlsSession value)
    {
        if (serviceScopeFactory.Create(value) is MediaController mediaController)
        {
            return mediaController;
        }

        return default;
    }
}
