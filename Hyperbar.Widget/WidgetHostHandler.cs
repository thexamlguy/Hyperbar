using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget;

public class WidgetHostHandler : 
    INotificationHandler<Create<IWidgetHost>>
{
    public async Task Handle(Create<IWidgetHost> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Value is IWidgetHost host)
        {
            if (host.Services.GetServices<IInitializer>() is
                IEnumerable<IInitializer> initializations)
            {
                foreach (IInitializer initialization in initializations)
                {
                    await initialization.InitializeAsync();
                }
            }
        }
    }
}