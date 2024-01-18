namespace Hyperbar;

public record Moved<TValue>(int Index, TValue Value) : INotification;