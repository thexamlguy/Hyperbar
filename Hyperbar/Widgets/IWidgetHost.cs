namespace Hyperbar;

public interface IWidgetHost : 
    IInitializer
{
    WidgetConfiguration Configuration { get; }

    IServiceProvider Services { get; }
}
