using System.Reflection;
using Windows.ApplicationModel.Resources.Core;
using Windows.Storage;

namespace Hyperbar.Widget.Windows;

internal class WidgetResourceInitialization(IWidgetAssembly widgetAssembly) :
    IInitializer
{
    public async Task InitializeAsync()
    {
        if (widgetAssembly.Assembly is Assembly assembly)
        {
            if (Path.GetDirectoryName(assembly.Location) is string assemblyDirectory)
            {
                FileInfo resourceFileInfo = new(Path.Combine(assemblyDirectory, 
                    "resources.pri"));

                if (!resourceFileInfo.Exists)
                {
                    resourceFileInfo = new(Path.Combine(assemblyDirectory, 
                        $"{assembly.GetName().Name}.pri"));
                }

                if (!resourceFileInfo.Exists)
                {
                    return;
                }

                StorageFile file = await StorageFile.GetFileFromPathAsync(resourceFileInfo.FullName);
                ResourceManager.Current.LoadPriFiles(new[] { file });
            }
        }
    }
}