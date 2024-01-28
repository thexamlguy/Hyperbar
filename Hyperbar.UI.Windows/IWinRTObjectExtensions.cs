using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using System.Reflection;
using System.Runtime.CompilerServices;
using WinRT;

namespace Hyperbar.UI.Windows;

public static class IWinRTObjectExtensions
{
    public static void InitializeComponent<TComponent>(this TComponent component,
        ref bool loaded, [CallerFilePath] string path = "")
        where TComponent : IWinRTObject
    {
        if (!loaded)
        {
            loaded = true;

            Type type = component.GetType();
            if (type.Assembly is Assembly assembly && Path.GetDirectoryName(assembly.Location) is string assemblyDirectory)
            {
                string resourceName = Path.GetFileNameWithoutExtension(path);
                string[] pathParts = path.Split(Path.DirectorySeparatorChar)[..^1];

                string? resourcePath = pathParts
                    .Reverse()
                    .Select(part => Path.Combine(assemblyDirectory, part, resourceName))
                    .FirstOrDefault(File.Exists);

                if (resourcePath is not null)
                {
                    Application.LoadComponent(component, new Uri($"ms-appx:///{resourcePath.Replace('\\', '/')}"),
                        ComponentResourceLocation.Nested);
                }
            }
        }
    }
}
