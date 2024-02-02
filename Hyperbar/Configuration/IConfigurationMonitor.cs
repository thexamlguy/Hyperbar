
namespace Hyperbar;

public interface IConfigurationMonitor<TConfiguration> : 
    IInitializer
    where TConfiguration :
    class;