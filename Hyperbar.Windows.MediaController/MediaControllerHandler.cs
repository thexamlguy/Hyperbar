using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Windows.MediaController;

public class MediaControllerHandler(IMediator mediator,
    IServiceScopeProvider<MediaController> scopeProvider) : 
    INotificationHandler<Created<MediaController>>
{
    public async ValueTask Handle(Created<MediaController> notification, 
        CancellationToken cancellationToken)
    {
        if (scopeProvider.TryGet(notification.Value, out IServiceScope? serviceScope))
        {
            if (serviceScope is not null)
            {
                if (serviceScope.ServiceProvider.GetService<IFactory<MediaControllerViewModel?>>()
                    is IFactory<MediaControllerViewModel?> factory)
                {
                    if (factory.Create() is MediaControllerViewModel mediaControllerViewModel)
                    {
                        await mediator.PublishAsync(new Created<MediaControllerViewModel>(mediaControllerViewModel),
                            cancellationToken);
                    }
                }
            }
        }
    }
}
