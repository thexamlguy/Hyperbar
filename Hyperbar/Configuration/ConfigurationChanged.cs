namespace Hyperbar;

public record ConfigurationChanged<TConfiguration>(TConfiguration Configuration) : INotification 
    where TConfiguration : 
    class;
