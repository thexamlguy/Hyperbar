namespace Hyperbar;

public interface IConfigurationReader<TConfiguration>
    where TConfiguration :
    class, new()
{
    bool TryRead(out TConfiguration? configuration);

    TConfiguration Read();
}
