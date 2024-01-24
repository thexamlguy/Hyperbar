namespace Hyperbar.Widget;

public interface IWidgetHost : 
    IInitializer
{
    WidgetConfiguration Configuration { get; }

    IServiceProvider Services { get; }
}
