namespace Hyperbar.Windows.MediaController;

public class MediaControllerViewModelFactory(IServiceFactory service) :
    IFactory<MediaControllerViewModel?>
{
    public MediaControllerViewModel? Create()
    {
        return service.Create<MediaControllerViewModel>();
    }
}
