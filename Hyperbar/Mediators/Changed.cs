namespace Hyperbar;

public record Changed<TValue>(TValue? Value = default) : INotification;