namespace Hyperbar;

public interface IRequest<out TResponse> : IMessage;

public interface IRequest : IRequest<Unit>;
