using Windows.ApplicationModel.Resources.Core;
using Windows.Storage;

namespace Hyperbar.Widget.Windows;

internal class WidgetResourceInitializer(IWidgetAssembly widgetAssembly) :
    IInitializer
{
    public async Task InitializeAsync()
    {
        string assemblyDirectory = Path.GetDirectoryName(widgetAssembly.Assembly.Location) ?? string.Empty;
        string[] possibleFileNames = ["resources.pri", $"{widgetAssembly.Assembly.GetName().Name}.pri"];

        FileInfo? resourceFileInfo = null;
        foreach (string fileName in possibleFileNames)
        {
            resourceFileInfo = new FileInfo(Path.Combine(assemblyDirectory, fileName));
            if (resourceFileInfo.Exists)
            {
                break;
            }
        }

        if (resourceFileInfo?.Exists is true)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(resourceFileInfo.FullName);
            ResourceManager.Current.LoadPriFiles(new[] { file });
        }
    }
}