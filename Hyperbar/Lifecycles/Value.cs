namespace Hyperbar;

public class Value<TValue>(IMediator mediator) :
    IValue<TValue>
    where TValue :
    notnull, new ()
{
    private TValue? current;

    public async Task SetAsync(Func<TValue, TValue> updateDelgate)
    {
        if (updateDelgate.Invoke(current ?? new TValue()) is TValue value)
        {
            if (current is null || !value.Equals(current))
            {
                current = value;
                await mediator.PublishAsync(new Changed<TValue>(current));
            }
        }
    }
}
