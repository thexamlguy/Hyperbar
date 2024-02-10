using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget;

public class WidgetStartedHandler(IPublisher publisher) :
    INotificationHandler<Started<IWidgetHost>>
{
    public async Task Handle(Started<IWidgetHost> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Value is IWidgetHost host)
        {
            if (host.Services.GetService<IWidgetViewModel>() is IWidgetViewModel viewModel)
            {
                await publisher.PublishAsync(new Create<IWidgetViewModel>(viewModel),
                    nameof(IWidgetHostViewModel), cancellationToken);
            }
        }
    }
}
