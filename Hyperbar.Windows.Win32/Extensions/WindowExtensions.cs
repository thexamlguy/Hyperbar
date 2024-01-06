using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using Windows.UI.Popups;
using WinRT.Interop;

namespace Hyperbar.Windows.Win32;

public static class WindowExtensions
{
    public static IntPtr GetHandle(this Window window) =>
        window is not null ? WindowNative.GetWindowHandle(window) : default;

    public static void SetIsShownInSwitchers(this Window window,
        bool value) => window.AppWindow.IsShownInSwitchers = value;

    public static void SetOpacity(this Window window,
        byte value) => window.GetHandle().SetWindowOpacity(value);

    public static void SetStyle(this Window window,
        WindowStyle style) => window.GetHandle().SetWindowStyle(style);

    public static void SetTopMost(this Window window,
        bool value)
    {
        AppWindow appWindow = window.AppWindow;
        if (appWindow.Presenter is OverlappedPresenter presenter)
        {
            presenter.IsAlwaysOnTop = value;
        }
    }

    public static void Snap(this Window window,
        WindowPlacement placement,
        double? width = null,
        double? height = null) => window.GetHandle().SnapWindow(placement, width, height);
}
