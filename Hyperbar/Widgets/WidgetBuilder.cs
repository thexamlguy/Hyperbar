using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public class WidgetBuilder(WidgetConfiguration configuration) : 
    IWidgetBuilder
{
    public IServiceCollection Services => new ServiceCollection();

    public WidgetConfiguration Configuration => configuration;

    public static IWidgetBuilder Configure<TWidgetConfiguration>(Action<TWidgetConfiguration> configurationDelegate)
        where TWidgetConfiguration :
        WidgetConfiguration,
        new()
    {
        TWidgetConfiguration configuration = new();
        configurationDelegate(configuration);

        return new WidgetBuilder(configuration);
    }
}