using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;

namespace Hyperbar.Interop.Windows;

public class WindowSnapping : 
    IDisposable
{
    private readonly uint callback;
    private readonly nint hwnd;
    private WindowSnappingPlacement placement;

    public WindowSnapping(IntPtr hwnd)
    {
        this.hwnd = hwnd;

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

    ~WindowSnapping()
    {
        Dispose();
    }

    public static WindowSnapping Create(IntPtr hwnd) => new(hwnd);

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Remove();
    }

    public void Snap(WindowSnappingPlacement placement,
        int size)
    {
        this.placement = placement;
        uint dpi = PInvoke.GetDpiForWindow(new HWND(hwnd));

        double scalingFactor = dpi / 96d;
        int actualSize = (int)(size * scalingFactor);

        if (Environment.Is64BitProcess)
        {
            APPBARDATA64 appBarData = GetAppBarData64();
            appBarData.rc = GetScreen();

            PInvoke.SHAppBarMessage(2, ref appBarData);

            switch (placement)
            {
                case WindowSnappingPlacement.Top:
                    appBarData.rc.bottom = appBarData.rc.top + actualSize;
                    break;
                case WindowSnappingPlacement.Bottom:
                    appBarData.rc.top = appBarData.rc.bottom - actualSize;
                    break;
                case WindowSnappingPlacement.Left:
                    appBarData.rc.right = appBarData.rc.left + actualSize;
                    break;
                case WindowSnappingPlacement.Right:
                    appBarData.rc.left = appBarData.rc.right - actualSize;
                    break;
                default: throw new NotSupportedException();
            }

            PInvoke.SHAppBarMessage(3, ref appBarData);
            PInvoke.SetWindowPos(new HWND(hwnd), new HWND(),
                appBarData.rc.left,
                appBarData.rc.top,
                appBarData.rc.right - appBarData.rc.left,
                appBarData.rc.bottom - appBarData.rc.top, 0);
        }
        else
        {
            APPBARDATA32 appBarData = GetAppBarData32();
            appBarData.rc = GetScreen();

            PInvoke.SHAppBarMessage(2, ref appBarData);

            switch (placement)
            {
                case WindowSnappingPlacement.Top:
                    appBarData.rc.bottom = appBarData.rc.top + actualSize;
                    break;
                case WindowSnappingPlacement.Bottom:
                    appBarData.rc.top = appBarData.rc.bottom - actualSize;
                    break;
                case WindowSnappingPlacement.Left:
                    appBarData.rc.right = appBarData.rc.left + actualSize;
                    break;
                case WindowSnappingPlacement.Right:
                    appBarData.rc.left = appBarData.rc.right - actualSize;
                    break;
                default: throw new NotSupportedException();
            }

            PInvoke.SHAppBarMessage(3, ref appBarData);
            PInvoke.SetWindowPos(new HWND(hwnd), new HWND(),
                appBarData.rc.left,
                appBarData.rc.top,
                appBarData.rc.right - appBarData.rc.left,
                appBarData.rc.bottom - appBarData.rc.top, 0);
        }
    }

    private APPBARDATA32 GetAppBarData32()
    {
        APPBARDATA32 appBarData = new();
        appBarData.cbSize = (uint)Marshal.SizeOf(appBarData);
        appBarData.hWnd = new HWND(hwnd);
        appBarData.uEdge = (uint)placement;

        return appBarData;
    }

    private APPBARDATA64 GetAppBarData64()
    {
        APPBARDATA64 appBarData = new();
        appBarData.cbSize = (uint)Marshal.SizeOf(appBarData);
        appBarData.hWnd = new HWND(hwnd);
        appBarData.uEdge = (uint)placement;

        return appBarData;
    }

    private RECT GetScreen()
    {
        Screen screen = Screen.FromHandle(hwnd);
        return new RECT
        {
            left = (int)screen.Bounds.Left,
            top = (int)screen.Bounds.Top,
            right = (int)screen.Bounds.Right,
            bottom = (int)screen.Bounds.Bottom
        };
    }

    private void Remove()
    {
        if (Environment.Is64BitProcess)
        {
            APPBARDATA64 appBarData = GetAppBarData64();
            PInvoke.SHAppBarMessage(2, ref appBarData);
        }
        else
        {
            APPBARDATA64 appBarData = GetAppBarData64();
            PInvoke.SHAppBarMessage(1, ref appBarData);
        }
    }
}
