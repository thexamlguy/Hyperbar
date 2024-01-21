namespace Hyperbar;

public interface IConfigurationWriter<TConfiguration>
    where TConfiguration :
    class
{
    void Write(Action<TConfiguration> updateDelegate);

    void Write(TConfiguration value);
}