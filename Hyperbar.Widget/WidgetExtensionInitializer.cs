namespace Hyperbar.Widget;

public class WidgetExtensionInitializer(IMediator mediator) :
    IInitializer
{
    public async Task InitializeAsync() => 
        await mediator.PublishAsync<Enumerate<WidgetExtension>>();
}