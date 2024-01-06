namespace Hyperbar;

public delegate ValueTask<TResponse> MessageHandlerDelegate<TMessage, TResponse>(TMessage message,
    CancellationToken cancellationToken)
    where TMessage :
    notnull,
    IMessage;
