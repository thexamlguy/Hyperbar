
namespace Hyperbar;

public record Navigate(object Key) :
    INotification;

public record Navigate<TView>(TView View, object ViewModel) :
    INotification;
