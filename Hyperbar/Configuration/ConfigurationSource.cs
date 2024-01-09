namespace Hyperbar;

public class ConfigurationSource<TConfiguration>(string path,
    string section) :
    IConfigurationSource<TConfiguration>
    where TConfiguration :
    class
{
    public string Path => path;

    public string Section => section;
}