using Hyperbar.Windows.Interop;

namespace Hyperbar.Windows;

public class KeyAcceleratorHandler(IVirtualKeyboard virtualKeyboard) :
    IRequestHandler<KeyAcceleratorCommand>
{
    public ValueTask<Unit> Handle(KeyAcceleratorCommand command,
        CancellationToken cancellationToken)
    {       
        virtualKeyboard.Send((int)command.Key);
        return default;
    }
}
