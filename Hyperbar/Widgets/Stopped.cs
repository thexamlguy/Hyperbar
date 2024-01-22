namespace Hyperbar;

public record Stopped<TValue>(TValue Value) : INotification;