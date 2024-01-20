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

public class WidgetMonitor :
    IInitializer
{
    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }
}

public class WidgetManager :
    IInitializer
{
    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }
}