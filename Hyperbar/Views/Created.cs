namespace Hyperbar;

public record Created<TValue>(TValue Value, object Target) : INotification
{
    public static Created<TValue> For<TTarget>(TValue value)
    {
        return new Created<TValue>(value, typeof(TTarget).Name);
    }
}
public record Inserted<TValue>(int Index, TValue Value, object Target) : INotification
{
    public static Inserted<TValue> For<TTarget>(int index, TValue value)
    {
        return new Inserted<TValue>(index, value, typeof(TTarget).Name);
    }
}