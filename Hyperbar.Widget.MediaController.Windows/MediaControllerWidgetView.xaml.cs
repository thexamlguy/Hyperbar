using CustomExtensions.WinUI;
using Microsoft.UI.Xaml.Controls;
using System.Reflection;
using System.Runtime.CompilerServices;
namespace Hyperbar.Widget.MediaController.Windows;

public sealed partial class MediaControllerWidgetView :
    UserControl
{
    public MediaControllerWidgetView()
    {
        Foo(@"C:\Users\dan_c\AppData\Local\Packages\24ccddba-447f-4d37-891d-523e8d820f45_rmhrgjnfy8he0\LocalCache\Local\Hyperbar.Windows\Extensions\Hyperbar.Windows.MediaController\Hyperbar.Windows.MediaController.dll");
        LocateResourcePath(this);
    }

    public Assembly ForeignAssembly { get; set; }

    private string ForeignAssemblyDir;
    private string ForeignAssemblyName;
    private bool? IsHotReloadAvailable;
    private DisposableCollection Disposables = new();
    private bool IsDisposed;

    internal static readonly Assembly? EntryAssembly;
    internal static readonly string HostingProcessDir;

    static MediaControllerWidgetView()
    {
        EntryAssembly = Assembly.GetEntryAssembly();
        HostingProcessDir = Path.GetDirectoryName(EntryAssembly.Location);
    }

    public void Foo(string assemblyPath)
    {
        // TODO: For some reason WinUI gets very angry when loading via AssemblyLoadContext,
        // even if using AssemblyLoadContext.Default which *should* have no difference than
        // Assembly.LoadFrom(), but it does.
        //
        // ExtensionContext = new(assemblyPath);
        // ForeignAssembly = ExtensionContext.LoadFromAssemblyPath(assemblyPath);
        ForeignAssembly = Assembly.LoadFrom(assemblyPath);
        ForeignAssemblyDir = Path.GetDirectoryName(ForeignAssembly.Location);
        ForeignAssemblyName = ForeignAssembly.GetName().Name;
    }

    private string LocateResourcePath(object component, [CallerFilePath] string callerFilePath = "")
    {
        if (component.GetType().Assembly != ForeignAssembly)
        {
            throw new InvalidProgramException();
        }
        string resourceName = Path.GetFileName(callerFilePath)[..^3];

        string[] pathParts = callerFilePath.Split('\\')[..^1];
        for (int i = pathParts.Length - 1; i > 1; i++)
        {
            string pathCandidate = Path.Join(pathParts[i..pathParts.Length].Append(resourceName).Prepend(ForeignAssemblyName).ToArray());
            FileInfo sourceResource = new(Path.Combine(ForeignAssemblyDir, pathCandidate));
            FileInfo colocatedResource = new(Path.Combine(HostingProcessDir, pathCandidate));
            if (colocatedResource.Exists)
            {
                return pathCandidate;
            }
            if (sourceResource.Exists)
            {
                return sourceResource.FullName;
            }

            throw new FileNotFoundException("Could not find resource", resourceName);
        }

        throw new FileNotFoundException("Could not find resource", resourceName);
    }
}