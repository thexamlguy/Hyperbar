using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Runtime.Loader;

namespace Hyperbar.Widget;

public class WidgetEnumeratorHandler(IHostEnvironment hostEnvironment, 
    IMediator mediator) :
    INotificationHandler<Enumerate<IWidget>>
{
    public Task Handle(Enumerate<IWidget> notification, 
        CancellationToken cancellationToken)
    {
        string extensionsDirectory = Path.Combine(hostEnvironment.ContentRootPath, "Extensions");
        if (Directory.Exists(extensionsDirectory))
        {
            List<string> assemblyPaths =
            [
                .. Directory.GetDirectories(extensionsDirectory)
                                .AsParallel()
                                .SelectMany(assemblyDirectory => Directory.GetFiles(assemblyDirectory, "*.dll"))
            ];

            Parallel.ForEach(assemblyPaths, (string assemblyPath) =>
            {
                Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
                mediator.PublishAsync(new Created<Assembly>(assembly));
            });
        }

        return Task.CompletedTask;
    }
}
