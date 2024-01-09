namespace Hyperbar;

public class Configuration<TConfiguration>(IConfigurationReader<TConfiguration> reader) :
    IConfiguration<TConfiguration>
    where TConfiguration :
    class, new()
{
    public TConfiguration Value => reader.Read();
}
