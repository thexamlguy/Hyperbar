namespace Hyperbar;

public record Created<TValue>(TValue Value) : INotification;

public record Inserted<TValue>(int Index, TValue Value) : INotification;