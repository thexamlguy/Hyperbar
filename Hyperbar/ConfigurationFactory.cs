namespace Hyperbar;

public class ConfigurationFactory<TConfiguration>(Func<TConfiguration> factory) :
    IConfigurationFactory<TConfiguration> 
    where TConfiguration :
    class
{
    public object Create() => factory.Invoke();

}
