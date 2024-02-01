namespace Hyperbar;

public class ConfigurationValueChangedNotification<TConfiguration, TValue>(IMediator mediator,
    Func<TConfiguration, Action<TValue>> factory) :
    IConfigurationValueChangedNotification<TConfiguration>
    where TConfiguration : 
    class
    where TValue : 
    class, new()
{
    private TValue? value;

    public async Task PublishAsync(TConfiguration configuration)
    {
        TValue newValue = new();
        factory(configuration).Invoke(newValue);

        if (value is null || !value.Equals(newValue))
        {
            value = newValue;
            await mediator.PublishAsync(new Changed<TValue>(value));
        }
    }
}
