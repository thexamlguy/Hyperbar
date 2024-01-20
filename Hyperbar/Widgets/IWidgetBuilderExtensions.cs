using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.Loader;

namespace Hyperbar;

public class AssemblyProvider
{
    public static void Load()
    {
        string extensionsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Extensions");
        Directory.CreateDirectory(extensionsPath);
    }

    public static IEnumerable<Assembly> Get(string path)
    {
        string extensionsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Extensions");

        Directory.CreateDirectory(extensionsPath);
        foreach (string assemblyPath in Directory.GetFiles(extensionsPath, "*.dll", SearchOption.AllDirectories))
        {
            yield return AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
        }
    }
}

public static class IWidgetBuilderExtensions
{
    public static IWidgetBuilder ConfigureServices(this IWidgetBuilder builder, 
        Action<IServiceCollection> servicesDelegate)
    {
        servicesDelegate(builder.Services);
        return builder;
    }
}
