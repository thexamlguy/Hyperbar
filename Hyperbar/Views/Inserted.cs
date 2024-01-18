namespace Hyperbar;

public record Inserted<TValue>(int Index, TValue Value) : INotification;