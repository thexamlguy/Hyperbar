
namespace Hyperbar;

public record Replaced<TValue>(int Index, TValue Value, object? Target = null) :
    INotification;

