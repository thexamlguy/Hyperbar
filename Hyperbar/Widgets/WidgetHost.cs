using Microsoft.Extensions.Hosting;

namespace Hyperbar;
public class WidgetHost(IHost host,
    IEnumerable<IInitializer> initializers,
    IProxyService<IMediator> proxyMediator) :
    IWidgetHost
{
    public IServiceProvider Services => host.Services;

    public async Task InitializeAsync()
    {
        foreach (IInitializer initializer in initializers)
        {
            await initializer.InitializeAsync();
        }

        //if (proxyMediator.Proxy is IMediator mediator)
        //{
        //    await mediator.PublishAsync(new Started<IWidgetHost>(this));
        //}
    }
}