using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget;

public class WidgetHostHandler : 
    INotificationHandler<Created<IWidgetHost>>
{
    public async Task Handle(Created<IWidgetHost> notification,
        CancellationToken cancellationToken)
    {
        if (notification.Value is IWidgetHost host)
        {
            if (host.Services.GetServices<IInitialization>() is
                IEnumerable<IInitialization> initializations)
            {
                foreach (IInitialization initialization in initializations)
                {
                    await initialization.InitializeAsync();
                }
            }
        }
    }
}