using Hyperbar.Interop.Windows;

namespace Hyperbar.Widget.Windows;

internal class KeyAcceleratorHandler(IVirtualKeyboard virtualKeyboard) :
    IHandler<KeyAccelerator>
{
    public Task<Unit> Handle(KeyAccelerator request,
        CancellationToken cancellationToken)
    {
        virtualKeyboard.Send((int)request.Key, request.Modifiers?.Select(modifier => (int)modifier).ToArray() ?? []);
        return Task.FromResult<Unit>(default);
    }
}
