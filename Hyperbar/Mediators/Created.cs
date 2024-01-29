
namespace Hyperbar;

public record Created<TValue>(TValue Value, object? Target = null) :
    INotification;