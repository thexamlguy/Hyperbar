namespace Hyperbar;

public class WidgetInitializer(IMediator mediator) :
    IInitializer
{
    public Task InitializeAsync()
    {
        mediator.PublishAsync<Enumerate<IWidget>>();
        return Task.CompletedTask;
    }
}