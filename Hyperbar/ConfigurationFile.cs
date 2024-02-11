using Microsoft.Extensions.FileProviders;

namespace Hyperbar;

public class ConfigurationFile<TConfiguration>(IFileInfo fileInfo) : 
    IConfigurationFile<TConfiguration>
    where TConfiguration :
    class
{
    public IFileInfo FileInfo => fileInfo;
}
