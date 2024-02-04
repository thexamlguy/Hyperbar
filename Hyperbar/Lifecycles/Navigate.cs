
namespace Hyperbar;

public record Navigate(object Key) :
    INotification;

public record Navigate<TTemplate>(TTemplate Template, object Content) :
    INotification;
