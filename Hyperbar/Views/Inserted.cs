namespace Hyperbar;

public record Inserted<TValue>(int Index, TValue Value, object Target) : INotification
{
    public static Inserted<TValue> For<TTarget>(int index, TValue value)
    {
        return new Inserted<TValue>(index, value, typeof(TTarget).Name);
    }
}