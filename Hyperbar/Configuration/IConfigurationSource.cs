namespace Hyperbar;

public interface IConfigurationSource<TConfiguration>
    where TConfiguration :
    class
{
    string Path { get; }

    string Section { get; }
}
