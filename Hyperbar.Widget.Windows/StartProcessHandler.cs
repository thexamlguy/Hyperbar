using System.Diagnostics;

namespace Hyperbar.Widget.Windows;

internal class StartProcessHandler :
    IHandler<StartProcess>
{
    public Task<Unit> Handle(StartProcess request,
        CancellationToken cancellationToken)
    {
        Process.Start(request.Process);
        return Task.FromResult<Unit>(default);
    }
}
