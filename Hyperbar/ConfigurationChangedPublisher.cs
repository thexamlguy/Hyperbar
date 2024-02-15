﻿namespace Hyperbar;

public class ConfigurationChangedPublisher<TConfiguration, TValue>(IPublisher publisher,
    Func<TConfiguration, Action<TValue>> factory) :
    IConfigurationChangedPublisher<TConfiguration>
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
            await publisher.PublishAsync(new Changed<TValue>(value));
        }
    }
}