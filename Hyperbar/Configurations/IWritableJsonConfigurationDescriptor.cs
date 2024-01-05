namespace Hyperbar.Configurations;

public interface IWritableJsonConfigurationDescriptor
{
    Type ConfigurationType { get; }

    string Key { get; }
}
