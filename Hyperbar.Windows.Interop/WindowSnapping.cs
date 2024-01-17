using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;

namespace Hyperbar.Windows.Interop;

public class WindowSnapping
{
    private readonly nint hwnd;
    private uint callback;

    public WindowSnapping(IntPtr hwnd)
    {
        this.hwnd = hwnd;
        InitializeAppBarWindow();
    }

    private enum AppBarMsg : int
    {
        ABM_NEW = 0,
        ABM_REMOVE,
        ABM_QUERYPOS,
        ABM_SETPOS,
        ABM_GETSTATE,
        ABM_GETTASKBARPOS,
        ABM_ACTIVATE,
        ABM_GETAUTOHIDEBAR,
        ABM_SETAUTOHIDEBAR,
        ABM_WINDOWPOSCHANGED,
        ABM_SETSTATE
    }
    public static WindowSnapping Create(IntPtr hwnd)
    {
        return new WindowSnapping(hwnd);
    }

    public void Snap(AppBarWindowPlacement placement, int size)
    {
        uint dpi = PInvoke.GetDpiForWindow(new HWND(hwnd));

        double scalingFactor = dpi / 96d;
        int actualSize = (int)(size * scalingFactor);

        Screen screen = Screen.FromHandle(hwnd);

        APPBARDATA32 appBarData = new();
        appBarData.cbSize = (uint)Marshal.SizeOf(appBarData);
        appBarData.hWnd = new HWND(hwnd);
        appBarData.uEdge = (uint)placement;
        appBarData.rc = new RECT
        {
            left = (int)screen.Bounds.Left,
            top = (int)screen.Bounds.Top,
            right = (int)screen.Bounds.Right,
            bottom = (int)screen.Bounds.Bottom
        };

        PInvoke.SHAppBarMessage((int)AppBarMsg.ABM_QUERYPOS, ref appBarData);

        switch (placement)
        {
            case AppBarWindowPlacement.Top:
                appBarData.rc.bottom = appBarData.rc.top + actualSize;
                break;
            case AppBarWindowPlacement.Bottom:
                appBarData.rc.top = appBarData.rc.bottom - actualSize;
                break;
            case AppBarWindowPlacement.Left:
                appBarData.rc.right = appBarData.rc.left + actualSize;
                break;
            case AppBarWindowPlacement.Right:
                appBarData.rc.left = appBarData.rc.right - actualSize;
                break;
            default: throw new NotSupportedException();
        }

        PInvoke.SHAppBarMessage((int)AppBarMsg.ABM_SETPOS, ref appBarData);

        PInvoke.SetWindowPos(new HWND(hwnd), new HWND(),
            appBarData.rc.left,
            appBarData.rc.top,
            appBarData.rc.right - appBarData.rc.left,
            appBarData.rc.bottom - appBarData.rc.top, 0);
    }

    private void InitializeAppBarWindow()
    {
        if (Environment.Is64BitProcess)
        {
            APPBARDATA64 appBarData = new();
            appBarData.cbSize = (uint)Marshal.SizeOf(appBarData);
            appBarData.hWnd = new HWND(hwnd);

            callback = PInvoke.RegisterWindowMessage("AppBarMessage64");
            appBarData.uCallbackMessage = callback;
            _ = PInvoke.SHAppBarMessage(0, ref appBarData);
        }
        else
        {
            APPBARDATA32 appBarData = new();
            appBarData.cbSize = (uint)Marshal.SizeOf(appBarData);
            appBarData.hWnd = new HWND(hwnd);

            callback = PInvoke.RegisterWindowMessage("AppBarMessage32");
            appBarData.uCallbackMessage = callback;

            _ = PInvoke.SHAppBarMessage(0, ref appBarData);
        }
    }
}
