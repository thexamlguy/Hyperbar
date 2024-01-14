using Windows.Media.Control;

namespace Hyperbar.Windows.MediaController;

public class MediaControllerFactory(IMediator mediator,
    IServiceScopeFactory<MediaController> serviceScopeFactory) :
    IFactory<GlobalSystemMediaTransportControlsSession, MediaController?>
{
    public MediaController? Create(GlobalSystemMediaTransportControlsSession value)
    {
        if (serviceScopeFactory.Create(value) is MediaController mediaController)
        {
            return mediaController;

            //if (serviceScope.ServiceProvider.GetService<IServiceFactory>() is IServiceFactory serviceFactory)
            //{
            //    if (serviceFactory.Create<MediaController>(value) is MediaController mediaController)
            //    {
            //        //if (serviceScope.ServiceProvider.GetService<IFactory<MediaControllerViewModel?>>()
            //        //    is IFactory<MediaControllerViewModel?> factory)
            //        //{
            //        //    if (factory.Create() is MediaControllerViewModel mediaControllerViewModel)
            //        //    {
            //        //        _ = await mediator.PublishAsync(new Created<MediaControllerViewModel>(mediaControllerViewModel));
            //        //    }
            //        //}

            //    }
            //}
        }

        return default;
    }
}
