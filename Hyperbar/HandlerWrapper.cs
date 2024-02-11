namespace Hyperbar;

public class HandlerWrapper<TMessage, TReply>(IHandler<TMessage, TReply> handler,
    IEnumerable<IPipelineBehavior<TMessage, TReply>> pipelineBehaviours)
    where TMessage : class, IRequest<TReply>
{
    private readonly IEnumerable<IPipelineBehavior<TMessage, TReply>> pipelineBehaviours = 
        pipelineBehaviours.Reverse();

    public async Task<TReply> Handle(TMessage message, CancellationToken cancellationToken)
    {
        HandlerDelegate<TMessage, TReply> currentHandler = handler.Handle;
        foreach (IPipelineBehavior<TMessage, TReply> behavior in pipelineBehaviours)
        {
            HandlerDelegate<TMessage, TReply> previousHandler = currentHandler;
            currentHandler = async (msg, token) =>
            {
                return await behavior.Handle(msg, previousHandler, token);
            };
        }

        return await currentHandler(message, cancellationToken);
    }
}

