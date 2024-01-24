namespace Hyperbar.Widget;

public record Stopped<TValue>(TValue Value) : INotification;