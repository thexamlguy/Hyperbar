namespace Hyperbar;

public interface ICommand : ICommand<Unit>;

public interface ICommand<out TResponse> : IMessage;

