using Windows.ApplicationModel.Resources.Core;
using Windows.Storage;

namespace Hyperbar.Widget;

public interface IWidgetResourceInitialization : 
    IInitializer
{

}

public class WidgetResourceInitialization :
    IInitializer
{
    public async Task InitializeAsync()
    {
        //FileInfo resourcePriFileInfo = new(Path.Combine(ForeignAssemblyDir, "resources.pri"));
        //if (!resourcePriFileInfo.Exists)
        //{
        //    resourcePriFileInfo = new(Path.Combine(ForeignAssemblyDir, $"{ForeignAssemblyName}.pri"));
        //}

        //if (!resourcePriFileInfo.Exists)
        //{
        //    return;
        //}

        //StorageFile file = await StorageFile.GetFileFromPathAsync(resourcePriFileInfo.FullName);
        //ResourceManager.Current.LoadPriFiles(new[] { file });
    }
}