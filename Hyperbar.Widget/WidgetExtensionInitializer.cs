namespace Hyperbar.Widget;

public class WidgetExtensionInitializer(IPublisher publisher) :
    IInitializer
{
    public async Task InitializeAsync() => 
        await publisher.PublishAsync<Enumerate<WidgetExtension>>();
}