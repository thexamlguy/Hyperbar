namespace Hyperbar;

public class NotficationPipelineHandler<TFromNotification, ToNotification>(IMediator mediator) : 
    INotificationHandler<TFromNotification>,
    IHandler
    where TFromNotification :
    INotification
    where ToNotification :
    INotification, new()
{
    private readonly IMediator mediator = mediator;

    public ValueTask Handle(TFromNotification notification, CancellationToken cancellationToken)
    {
        return mediator.PublishAsync(new ToNotification(), 
            cancellationToken);
    }
}
