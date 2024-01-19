
namespace Hyperbar;

public class WidgetContext(IServiceProvider serviceProvider) : 
    IInitializer
{
    public IServiceProvider ServiceProvider => serviceProvider;

    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }
}