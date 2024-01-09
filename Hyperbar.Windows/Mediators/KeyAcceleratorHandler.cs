using Hyperbar.Windows.Interop;
using System.Diagnostics;

namespace Hyperbar.Windows;

public class KeyAcceleratorHandler(IVirtualKeyboard virtualKeyboard) :
    IRequestHandler<KeyAcceleratorRequest>
{
    public ValueTask<Unit> Handle(KeyAcceleratorRequest request,
        CancellationToken cancellationToken)
    {
        virtualKeyboard.Send((int)request.Key, request.Modifiers?.Select(modifier => (int)modifier).ToArray() ?? []);
        return default;
    }
}

public class ProcesssAcceleratorHandler :
    IRequestHandler<ProcessRequest>
{
    public ValueTask<Unit> Handle(ProcessRequest request,
        CancellationToken cancellationToken)
    {
        Process.Start(request.Process);
        return default;
    }
}
