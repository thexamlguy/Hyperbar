using System.Runtime.CompilerServices;
using WinRT;

namespace Hyperbar.UI.Windows;

public static class IWinRTObjectExtensions
{
    public static void InitializeComponent<TComponent>(this TComponent component, 
        ref bool loaded, 
        [CallerFilePath] string path = "") 
        where TComponent : 
        IWinRTObject
    {
        if (loaded)
        {
            return;
        }

        loaded = true;

        //Uri resourceLocator = ApplicationExtensionHost.Current.LocateResource(component, callerFilePath);
        //Application.LoadComponent(component, resourceLocator, ComponentResourceLocation.Nested);
    }
}
