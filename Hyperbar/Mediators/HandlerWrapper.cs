namespace Hyperbar;

public class HandlerWrapper<TRequest, TResponse>
    where TRequest :
    class,
    IRequest<TResponse>
{
    private readonly HandlerDelegate<TRequest, TResponse> handler;

    public HandlerWrapper(IHandler<TRequest, TResponse> concreteHandler,
        IEnumerable<IPipelineBehavior<TRequest, TResponse>> pipelineBehaviours)
    {
        HandlerDelegate<TRequest, TResponse> handler = concreteHandler.Handle;
        foreach (IPipelineBehavior<TRequest, TResponse>? pipeline in pipelineBehaviours.Reverse())
        {
            HandlerDelegate<TRequest, TResponse> handlerCopy = handler;
            IPipelineBehavior<TRequest, TResponse> pipelineCopy = pipeline;

            handler = (TRequest message, CancellationToken cancellationToken) =>
                pipelineCopy.Handle(message, handlerCopy, cancellationToken);
        }

        this.handler = handler;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken) => handler(request, cancellationToken);
}