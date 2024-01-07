namespace Hyperbar;

public interface IHandler;

public interface IHandler<in TRequest, TResponse> :
    IHandler
    where TRequest :
    IRequest<TResponse>
{
    ValueTask<TResponse> Handle(TRequest request,
        CancellationToken cancellationToken);
}

public interface IRequestHandler<in TRequest> : 
    IHandler<TRequest, Unit>
    where TRequest :
    IRequest<Unit>
{
}