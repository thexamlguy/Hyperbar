
namespace Hyperbar;

public class NotficationRelayHandler<TFromNotification, ToNotification>(IMediator mediator) :
    INotificationHandler<TFromNotification>,
    IHandler
    where TFromNotification :
    INotification
    where ToNotification :
    INotification, new()
{
    private readonly IMediator mediator = mediator;

    public Task Handle(TFromNotification notification, CancellationToken cancellationToken) =>
        mediator.PublishAsync<ToNotification>(cancellationToken);
}