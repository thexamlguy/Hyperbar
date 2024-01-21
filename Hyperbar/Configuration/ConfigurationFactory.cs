namespace Hyperbar;

public class ConfigurationFactory<TConfiguration>(Func<TConfiguration> factory) :
    IConfigurationFactory<TConfiguration> 
    where TConfiguration :
    class
{
    public TConfiguration Create() => factory.Invoke();
}
