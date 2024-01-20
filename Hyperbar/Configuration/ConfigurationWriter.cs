namespace Hyperbar;

public class ConfigurationWriter<TConfiguration>(IConfigurationSource<TConfiguration> source) :
    IConfigurationWriter<TConfiguration>
    where TConfiguration :
    class, new()
{
    public void Write(Action<TConfiguration> updateDelegate)
    {
        if ((source.TryGet(out TConfiguration? value) ? value : new TConfiguration()) is TConfiguration updatedValue)
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