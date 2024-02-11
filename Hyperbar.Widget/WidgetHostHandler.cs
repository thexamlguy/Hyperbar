using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar.Widget;

public class WidgetHostHandler(IWidgetHostCollection widgetHosts) : 
    INotificationHandler<Create<IWidgetHost>>
{
    public async Task Handle(Create<IWidgetHost> args,
        CancellationToken cancellationToken)
    {
        if (args.Value is IWidgetHost host)
        {
            widgetHosts.Add(host);

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