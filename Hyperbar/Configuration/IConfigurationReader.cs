namespace Hyperbar;

public interface IConfigurationReader<TConfiguration>
    where TConfiguration :
    class
{
    bool TryRead(out TConfiguration? configuration);

    TConfiguration Read();
}
