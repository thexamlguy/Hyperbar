using Microsoft.Extensions.DependencyInjection;

namespace Hyperbar;

public class WidgetBuilder
{
    public static void Config(IServiceCollection services, 
        Action<IWidget> widgetDelegate)
    {
        Widget widget = new();
        widgetDelegate(widget);

        services.AddSingleton(widget);
    }
}