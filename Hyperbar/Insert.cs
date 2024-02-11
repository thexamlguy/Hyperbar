namespace Hyperbar;

public record Insert<TValue>(int Index, TValue Value) : INotification;
