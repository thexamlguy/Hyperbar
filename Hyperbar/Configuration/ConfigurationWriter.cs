namespace Hyperbar;

public class ConfigurationWriter<TConfiguration>(IConfigurationSource<TConfiguration> source) :
    IConfigurationWriter<TConfiguration>
    where TConfiguration :
    class
{
    public void Write(Action<TConfiguration> updateDelegate)
    {
        if (source.TryGet(out TConfiguration? value) is TConfiguration updatedValue)
        {
            updateDelegate?.Invoke(updatedValue);
            Write(updatedValue);
        }
    }

    public void Write(object value) => source.Set(value);

    public void Write(TConfiguration value) => source.Set(value);
}