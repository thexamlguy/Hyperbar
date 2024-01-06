﻿namespace Hyperbar;

public class CommandClassHandlerWrapper<TRequest, TResponse> 
    where TRequest : 
    class,
    ICommand<TResponse>
{
    private readonly MessageHandlerDelegate<TRequest, TResponse> handler;

    public CommandClassHandlerWrapper(ICommandHandler<TRequest, TResponse> concreteHandler,
        IEnumerable<IPipelineBehavior<TRequest, TResponse>> pipelineBehaviours)
    {
        MessageHandlerDelegate<TRequest, TResponse> handler = concreteHandler.Handle;

        foreach (IPipelineBehavior<TRequest, TResponse>? pipeline in pipelineBehaviours.Reverse())
        {
            MessageHandlerDelegate<TRequest, TResponse> handlerCopy = handler;
            IPipelineBehavior<TRequest, TResponse> pipelineCopy = pipeline;
            handler = (TRequest message, CancellationToken cancellationToken) => 
                pipelineCopy.Handle(message, handlerCopy, cancellationToken);
        }

        this.handler = handler;
    }

    public ValueTask<TResponse> Handle(TRequest request, CancellationToken cancellationToken) => handler(request, cancellationToken);
}
