namespace Hyperbar;

public record Created<TValue>(TValue Value, object Target) : INotification
{
    public static Created<TValue> For<TTarget>(TValue value)
    {
        return new Created<TValue>(value, typeof(TTarget).Name);
    }
}