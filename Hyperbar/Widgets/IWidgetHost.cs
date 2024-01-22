namespace Hyperbar;

public interface IWidgetHost : 
    IInitializer
{
    IServiceProvider Services { get; }
}
