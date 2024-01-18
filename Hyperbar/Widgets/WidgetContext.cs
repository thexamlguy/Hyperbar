namespace Hyperbar;

public sealed class WidgetContext(IServiceProvider serviceProvider) 
{
    public IServiceProvider ServiceProvider => serviceProvider;
}