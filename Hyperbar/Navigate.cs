namespace Hyperbar;

public record Navigate(object Name, string? TargetName = null) :
    INotification;

public record Navigate<TNavigation>(object Target, object View, object ViewModel) :
    INotification;
