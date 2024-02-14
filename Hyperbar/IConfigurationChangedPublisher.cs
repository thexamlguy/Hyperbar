namespace Hyperbar;

public interface IConfigurationChangedPublisher<TConfiguration>
{
    Task PublishAsync(TConfiguration configuration);
}
