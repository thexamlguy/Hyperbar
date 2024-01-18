namespace Hyperbar;

public interface IPipelineBehavior<TMessage, TResponse>
    where TMessage :
    notnull,
    IMessage
{
    Task<TResponse> Handle(TMessage message,
        HandlerDelegate<TMessage, TResponse> next,
        CancellationToken cancellationToken = default);
}