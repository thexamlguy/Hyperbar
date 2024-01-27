using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Runtime.Loader;

namespace Hyperbar.Widget;

public class WidgetEnumerator(IFactory<Type, IWidget> factory,
    IHostEnvironment hostEnvironment, 
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

            Parallel.ForEach(assemblyPaths, async (string assemblyPath) =>
            {
                Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
                if (assembly.GetTypes().FirstOrDefault(x => typeof(IWidget).IsAssignableFrom(x)) is Type widgetType)
                {
                    if (factory.Create(widgetType) is IWidget widget)
                    {
                       await mediator.PublishAsync(new Created<IWidget>(widget),
                           cancellationToken);
                    }
                }
            });
        }

        return Task.CompletedTask;
    }
}
