namespace Hyperbar.Windows.MediaController;

public class MediaControllerViewModelFactory(IServiceFactory service, ICache<MediaControllerViewModel> cache) :
    IFactory<MediaControllerViewModel?>
{
    public MediaControllerViewModel? Create()
    {
        if (service.Create<MediaControllerViewModel>() is MediaControllerViewModel widgetComponentViewModel)
        {
            cache.Add(widgetComponentViewModel);
            return widgetComponentViewModel;
        }

        return default;
    }
}
