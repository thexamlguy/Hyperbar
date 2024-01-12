namespace Hyperbar;

public record Created<TValue>(TValue? Value = default) : INotification;
