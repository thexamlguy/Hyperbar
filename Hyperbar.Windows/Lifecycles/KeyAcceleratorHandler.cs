using Hyperbar.Windows.Interop;

namespace Hyperbar.Windows;

public class KeyAcceleratorHandler(IVirtualKeyboard virtualKeyboard) :
    IHandler<KeyAccelerator>
{
    public Task<Unit> Handle(KeyAccelerator request,
        CancellationToken cancellationToken)
    {
        virtualKeyboard.Send((int)request.Key, request.Modifiers?.Select(modifier => (int)modifier).ToArray() ?? []);
        return Task.FromResult<Unit>(default);
    }
}
