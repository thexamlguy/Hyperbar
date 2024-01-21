namespace Hyperbar;

public class ConfigurationWriter<TConfiguration>(IConfigurationSource<TConfiguration> source,
    IConfigurationFactory<TConfiguration> factory) :
    IConfigurationWriter<TConfiguration>
    where TConfiguration :
    class
{
    public void Write(Action<TConfiguration> updateDelegate)
    {
        if ((source.TryGet(out TConfiguration? value) ? value : 
            factory.Create()) is TConfiguration updatedValue)
        {
            updateDelegate?.Invoke(updatedValue);
            Write(updatedValue);
        }
    }

    public void Write(TConfiguration value)
    {
        source.Set(value);
    }
}