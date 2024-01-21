using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public class WidgetServiceCollection :
    IWidgetServiceCollection
{
    public WidgetServiceCollection(Action<IServiceCollection> configureDelegate)
    {
        Services = new ServiceCollection();
        configureDelegate.Invoke(Services);
    }

    public IServiceCollection Services { get; private set; }
}