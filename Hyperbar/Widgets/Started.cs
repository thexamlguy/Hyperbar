namespace Hyperbar;

public record Started<TValue>(TValue Value) : INotification;
