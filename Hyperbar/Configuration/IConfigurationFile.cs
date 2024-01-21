using Microsoft.Extensions.FileProviders;

namespace Hyperbar;

public interface IConfigurationFile<TConfiguration>
    where TConfiguration :
    class
{
    IFileInfo FileInfo { get; }
}
