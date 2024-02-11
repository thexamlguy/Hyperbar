namespace Hyperbar;

public record Move<TValue>(int Index, TValue Value) : INotification;