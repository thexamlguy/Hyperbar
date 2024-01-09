namespace Hyperbar;

public interface IConfigurationReader<TConfiguration>
    where TConfiguration :
    class, new()
{
    TConfiguration Read();
}
