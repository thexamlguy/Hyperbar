using System.Diagnostics;

namespace Hyperbar.Windows;

public class StartProcessHandler :
    IRequestHandler<StartProcess>
{
    public ValueTask<Unit> Handle(StartProcess request,
        CancellationToken cancellationToken)
    {
        Process.Start(request.Process);
        return default;
    }
}
