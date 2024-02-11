namespace Hyperbar
{
    public interface IConfigurationFactory<TConfiguration> 
        where TConfiguration : 
        class
    {
        object Create();
    }
}