using Hyperbar.Windows.Interop;

namespace Hyperbar.Windows;

public class KeyAcceleratorHandler(IVirtualKeyboard virtualKeyboard) :
    IRequestHandler<KeyAccelerator>
{
    public ValueTask<Unit> Handle(KeyAccelerator request,
        CancellationToken cancellationToken)
    {
        virtualKeyboard.Send((int)request.Key, request.Modifiers?.Select(modifier => (int)modifier).ToArray() ?? []);
        return default;
    }
}
