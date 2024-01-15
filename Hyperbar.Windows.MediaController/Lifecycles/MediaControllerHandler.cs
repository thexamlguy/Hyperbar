using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Windows.MediaController;

public class MediaControllerHandler(IMediator mediator,
    IServiceScopeProvider<MediaController> scopeProvider,
    ICache<MediaController, MediaControllerViewModel> cache) : 
    INotificationHandler<Created<MediaController>>
{
    public async ValueTask Handle(Created<MediaController> notification, 
        CancellationToken cancellationToken)
    {
        if (notification.Value is MediaController mediaController)
        {
            if (scopeProvider.TryGet(mediaController, out IServiceScope? serviceScope))
            {
                if (serviceScope is not null)
                {
                    if (serviceScope.ServiceProvider.GetService<IFactory<MediaController, MediaControllerViewModel?>>()
                        is IFactory<MediaController, MediaControllerViewModel?> factory)
                    {
                        if (factory.Create(mediaController) is MediaControllerViewModel mediaControllerViewModel)
                        {
                            cache.Add(mediaController, mediaControllerViewModel);
                            await mediator.PublishAsync(new Created<MediaControllerViewModel>(mediaControllerViewModel),
                                cancellationToken);
                        }
                    }
                }
            }
        }

    }
}
