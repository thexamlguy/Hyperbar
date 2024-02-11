namespace Hyperbar;

public interface IConfigurationValueChangedNotification<TConfiguration>
{
    Task PublishAsync(TConfiguration configuration);
}
