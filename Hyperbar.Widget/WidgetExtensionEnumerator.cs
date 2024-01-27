using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Runtime.Loader;

namespace Hyperbar.Widget;

public class WidgetExtensionEnumerator(IFactory<Type, IWidget> factory,
    IHostEnvironment hostEnvironment, 
    IMediator mediator) :
    INotificationHandler<Enumerate<WidgetExtension>>
{
    public Task Handle(Enumerate<WidgetExtension> notification, 
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
                foreach (Type widgetType in assembly.GetTypes().Where(x => typeof(IWidget).IsAssignableFrom(x)))
                {
                    if (factory.Create(widgetType) is IWidget widget)
                    {
                        await mediator.PublishAsync(new Created<WidgetExtension>(new WidgetExtension(widget, 
                            new WidgetAssembly(assembly))), cancellationToken);
                    }
                }
            });
        }

        return Task.CompletedTask;
    }
}
