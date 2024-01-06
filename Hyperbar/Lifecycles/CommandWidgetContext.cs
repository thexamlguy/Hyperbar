namespace Hyperbar.Lifecycles;

public class CommandWidgetContext(IServiceProvider serviceProvider) :
    ICommandWidgetContext
{
    public IServiceProvider ServiceProvider => serviceProvider;
}