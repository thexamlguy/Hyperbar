using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget;

public class WidgetHostHandler(IMediator mediator) :
    INotificationHandler<Started<IWidgetHost>>,
    INotificationHandler<Stopped<IWidgetHost>>
{
    public async Task Handle(Started<IWidgetHost> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Value is IWidgetHost host)
        {
            if (host.Services.GetService<IWidgetViewModel>() is IWidgetViewModel viewModel)
            {
                await mediator.PublishAsync(new Created<IWidgetViewModel>(viewModel),
                    nameof(WidgetViewModel), cancellationToken);
            }
        }
    }

    public Task Handle(Stopped<IWidgetHost> notification,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}