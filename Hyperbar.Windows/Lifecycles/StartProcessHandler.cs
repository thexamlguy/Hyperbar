using System.Diagnostics;

namespace Hyperbar.Windows;

public class StartProcessHandler :
    IHandler<StartProcess>
{
    public Task<Unit> Handle(StartProcess request,
        CancellationToken cancellationToken)
    {
        Process.Start(request.Process);
        return Task.FromResult<Unit>(default);
    }
}
