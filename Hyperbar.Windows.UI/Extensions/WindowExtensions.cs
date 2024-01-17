using Hyperbar.Windows.Interop;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.Graphics;
using WinRT.Interop;

namespace Hyperbar.Windows.UI;

public static class WindowExtensions
{
    public static WindowMessageListener CreateMessageListener(this Window window) =>
        WindowMessageListener.Create(window.GetHandle());

    public static IntPtr GetHandle(this Window window) =>
            window is not null ? WindowNative.GetWindowHandle(window) : default;

    public static void MoveAndResize(this Window window,
        double x,
        double y,
        double width,
        double height)
    {
        float num = HwndExtensions.GetDpiForWindow(window.GetHandle()) / 96f;
        window.AppWindow.MoveAndResize(new RectInt32((int)x, (int)y, (int)(width * (double)num), (int)(height * (double)num)));
    }

    public static void SetIsAvailableInSwitchers(this Window window,
            bool value) => window.AppWindow.IsShownInSwitchers = value;

    public static void SetOpacity(this Window window,
        byte value) => window.GetHandle().SetWindowOpacity(value);

    public static void SetStyle(this Window window,
        WindowStyle style) => window.GetHandle().SetWindowStyle(style);

    public static void SetStyle(this Window window,
        ExtendedWindowStyle style) => window.GetHandle().SetExtendedWindowStyle(style);

    public static void SetTopMost(this Window window,
        bool value)
    {
        AppWindow appWindow = window.AppWindow;
        if (appWindow.Presenter is OverlappedPresenter presenter)
        {
            presenter.IsAlwaysOnTop = value;
        }
    }
}