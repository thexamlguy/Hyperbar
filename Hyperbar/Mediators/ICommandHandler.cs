namespace Hyperbar;

public record KeyAcceleratorCommand(string Key, string[]? Modifiers = null) :
    ICommand;

public class KeyAcceleratorCommandHanler :
    ICommandHandler<KeyAcceleratorCommand>
{
    public ValueTask<Unit> Handle(KeyAcceleratorCommand command,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
    where TCommand :
    ICommand<Unit>;

public interface ICommandHandler<in TCommand, TResponse>
    where TCommand :
    ICommand<TResponse>
{
    ValueTask<TResponse> Handle(TCommand command,
        CancellationToken cancellationToken);
}