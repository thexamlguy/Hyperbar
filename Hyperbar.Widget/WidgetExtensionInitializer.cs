namespace Hyperbar.Widget;

public class WidgetExtensionInitializer(IMediator mediator) :
    IInitialization
{
    public async Task InitializeAsync() => 
        await mediator.PublishAsync<Enumerate<WidgetExtension>>();
}