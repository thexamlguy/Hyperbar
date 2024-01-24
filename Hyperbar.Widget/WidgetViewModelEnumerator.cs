namespace Hyperbar.Widget;

public class WidgetViewModelEnumerator :
    INotificationHandler<Enumerate<IWidgetViewModel>>
{
    public Task Handle(Enumerate<IWidgetViewModel> notification,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}