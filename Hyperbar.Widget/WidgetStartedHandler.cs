using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget;

public class WidgetStartedHandler(IMediator mediator) :
    INotificationHandler<Started<IWidgetHost>>
{
    public async Task Handle(Started<IWidgetHost> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Value is IWidgetHost host)
        {
            if (host.Services.GetService<IWidgetViewModel>() is IWidgetViewModel viewModel)
            {
                await mediator.PublishAsync(new Created<IWidgetViewModel>(viewModel),
                    nameof(IWidgetHostViewModel), cancellationToken);
            }
        }
    }
}
