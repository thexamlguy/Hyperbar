namespace Hyperbar.Configurations;

public record WritableJsonConfigurationDescriptor(Type ConfigurationType, string Key) : IWritableJsonConfigurationDescriptor;
