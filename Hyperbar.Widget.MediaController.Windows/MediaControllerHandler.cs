using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget.MediaController.Windows;

public class MediaControllerHandler(IMediator mediator,
    IServiceScopeProvider<MediaController> scopeProvider,
    ICache<MediaController, MediaControllerViewModel> cache) :
    INotificationHandler<Created<MediaController>>,
    INotificationHandler<Removed<MediaController>>
{
    public async Task Handle(Created<MediaController> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Value is MediaController mediaController &&
            scopeProvider.TryGet(mediaController, out IServiceScope? serviceScope) &&
            serviceScope?.ServiceProvider.GetService<IFactory<MediaController, MediaControllerViewModel?>>() 
                is IFactory<MediaController, MediaControllerViewModel?> factory &&
            factory.Create(mediaController) is MediaControllerViewModel viewModel)
        {
            cache.Add(mediaController, viewModel);
            await mediator.PublishAsync(new Created<MediaControllerViewModel>(viewModel), cancellationToken);
        }
    }

    public async Task Handle(Removed<MediaController> notification, CancellationToken cancellationToken)
    {
        if (notification.Value is MediaController mediaController &&
            cache.TryGetValue(mediaController, out MediaControllerViewModel? viewModel) &&
            viewModel is not null)
        {
            await mediator.PublishAsync(new Removed<MediaControllerViewModel>(viewModel), cancellationToken);
            cache.Remove(mediaController);
        }
    }
}