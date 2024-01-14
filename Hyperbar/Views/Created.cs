namespace Hyperbar;

public record Created<TValue>(TValue Value) : INotification;