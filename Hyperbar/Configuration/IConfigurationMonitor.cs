
namespace Hyperbar;

public interface IConfigurationMonitor<TConfiguration> : 
    IInitialization
    where TConfiguration :
    class;