using Hyperbar.Windows.Interop;

namespace Hyperbar.Windows;

public class KeyAcceleratorCommandHandler(IVirtualKeyboard virtualKeyboard) :
    ICommandHandler<KeyAcceleratorCommand>
{
    public ValueTask<Unit> Handle(KeyAcceleratorCommand command,
        CancellationToken cancellationToken)
    {        virtualKeyboard.Send((int)command.Key);
        return default;
    }
}
