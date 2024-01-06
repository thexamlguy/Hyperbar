namespace Hyperbar;

public class WidgetContext(IServiceProvider serviceProvider) :
    IWidgetContext
{
    public IServiceProvider ServiceProvider => serviceProvider;
}