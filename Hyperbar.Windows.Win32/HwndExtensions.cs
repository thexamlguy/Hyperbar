using System;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Hyperbar.Windows.Win32;

public static class HwndExtensions
{
    [Flags]
    private enum WindowStyles
    {
        WS_EX_LAYERED = 0x80000
    }

    public static void SetWindowOpacity(this IntPtr hWnd,
        byte value)
    {
        HWND hWND = new(hWnd);
        WindowStyles windowLong = (WindowStyles)PInvoke.GetWindowLong(hWND, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE);
        if (!windowLong.HasFlag(WindowStyles.WS_EX_LAYERED))
        {
            PInvoke.SetWindowLong(hWND, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE, (int)(windowLong | WindowStyles.WS_EX_LAYERED));
        }

        if (!PInvoke.SetLayeredWindowAttributes(hWND, 0u, value, LAYERED_WINDOW_ATTRIBUTES_FLAGS.LWA_ALPHA))
        {
            Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
        }
    }

    public static void SetWindowStyle(this IntPtr hwnd,
        WindowStyle newStyle)
    {
        int windowLong = PInvoke.GetWindowLong(new HWND(hwnd), WINDOW_LONG_PTR_INDEX.GWL_STYLE);
        if (PInvoke.SetWindowLong(new HWND(hwnd), WINDOW_LONG_PTR_INDEX.GWL_STYLE, (int)newStyle) != windowLong)
        {
            Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
        }

        PInvoke.SetWindowPos(new HWND(hwnd), new HWND(0), 0, 0, 0, 0, SET_WINDOW_POS_FLAGS.SWP_DRAWFRAME | SET_WINDOW_POS_FLAGS.SWP_NOMOVE | SET_WINDOW_POS_FLAGS.SWP_NOOWNERZORDER | SET_WINDOW_POS_FLAGS.SWP_NOSIZE | SET_WINDOW_POS_FLAGS.SWP_NOZORDER);
    }

    public static void SnapWindow(this IntPtr hwnd,
        WindowPlacement placement,
        double? width = null,
        double? height = null)
    {
        HMONITOR hwndDesktop = PInvoke.MonitorFromWindow(new(hwnd), MONITOR_FROM_FLAGS.MONITOR_DEFAULTTONEAREST);
        MONITORINFO info = new()
        {
            cbSize = 40
        };

        PInvoke.GetMonitorInfo(hwndDesktop, ref info);

        uint dpi = PInvoke.GetDpiForWindow(new HWND(hwnd));
        PInvoke.GetWindowRect(new HWND(hwnd), out RECT windowRect);

        double scalingFactor = dpi / 96d;
        int actualWidth = width.HasValue ? (int)(width * scalingFactor) : windowRect.right - windowRect.left;
        int actualHeight = height.HasValue ? (int)(height * scalingFactor) : windowRect.bottom - windowRect.top;

        int left = 0;
        int top = 0;

        switch (placement)
        {
            case WindowPlacement.Left:
                left = 0;
                top = (info.rcWork.bottom + info.rcWork.top) / 2 - actualHeight / 2;
                break;

            case WindowPlacement.Top:
                left = (info.rcWork.left + info.rcWork.right) / 2 - actualWidth / 2;
                top = 0;
                break;

            case WindowPlacement.Right:
                left = info.rcWork.left + info.rcWork.right - actualWidth;
                top = (info.rcWork.bottom + info.rcWork.top) / 2 - actualHeight / 2;
                break;

            case WindowPlacement.Bottom:
                left = (info.rcWork.left + info.rcWork.right) / 2 - actualWidth / 2;
                top = info.rcWork.bottom + info.rcWork.top - actualHeight;
                break;
        }

        PInvoke.SetWindowPos(new HWND(hwnd), new HWND(), left, top, actualWidth, actualHeight, 0);
    }
}