
namespace Hyperbar;

public record Create<TValue>(TValue Value, object? Target = null) :
    INotification;