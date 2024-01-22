namespace Hyperbar;

public interface IWidgetHost
{
    IServiceProvider Services { get; }

    Task StartAsync();

    Task StopAsync();
}
