namespace Hyperbar;

public class ConfigurationReader<TConfiguration>(IConfigurationSource<TConfiguration> source) :
    IConfigurationReader<TConfiguration>
    where TConfiguration :
    class, new()
{
    public TConfiguration Read()
    {
        if ((source.TryGet(out TConfiguration? value) ? value :
            new TConfiguration()) is TConfiguration configuration)
        {
            return configuration;
        }

        return new TConfiguration();
    }

    public bool TryRead(out TConfiguration? configuration)
    {
        if (source.TryGet(out TConfiguration? value) && value is not null)
        {
            configuration = value;
            return true;
        }

        configuration = default;
        return false;
    }
}
