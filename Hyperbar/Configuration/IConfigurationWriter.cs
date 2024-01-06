namespace Hyperbar;

public interface IConfigurationWriter<TConfiguration>
    where TConfiguration : 
    class, new()
{
    void Write(Action<TConfiguration> updateDelegate);

    void Write(TConfiguration value);
}