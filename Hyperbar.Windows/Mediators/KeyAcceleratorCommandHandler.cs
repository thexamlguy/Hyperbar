namespace Hyperbar.Windows;

public class KeyAcceleratorCommandHandler :
    ICommandHandler<KeyAcceleratorCommand>
{
    public ValueTask<Unit> Handle(KeyAcceleratorCommand command,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
