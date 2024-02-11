namespace Hyperbar;

public interface IConfigurationInitializer<TConfiguration> 
    where TConfiguration :
    class
{
    Task InitializeAsync();
}