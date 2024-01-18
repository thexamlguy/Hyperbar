namespace Hyperbar;

public delegate Task<TResponse> HandlerDelegate<TMessage, TResponse>(TMessage message,
    CancellationToken cancellationToken)
    where TMessage :
    notnull,
    IMessage;