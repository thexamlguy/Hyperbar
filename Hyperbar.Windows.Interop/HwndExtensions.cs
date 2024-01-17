using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Hyperbar.Windows.Interop;

public static class HwndExtensions
{
    [Flags]
    private enum WindowStyles
    {
        WS_EX_LAYERED = 0x80000
    }

    public static void SetExtendedWindowStyle(this IntPtr hwnd, ExtendedWindowStyle newStyle)
    {
        int windowLong = PInvoke.GetWindowLong(new(hwnd), WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE);
        if (PInvoke.SetWindowLong(new(hwnd), WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE, (int)newStyle) != windowLong)
        {
            Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
        }

        PInvoke.SetWindowPos(new(hwnd), new HWND(0), 0, 0, 0, 0, SET_WINDOW_POS_FLAGS.SWP_DRAWFRAME |
            SET_WINDOW_POS_FLAGS.SWP_NOMOVE |
            SET_WINDOW_POS_FLAGS.SWP_NOOWNERZORDER |
            SET_WINDOW_POS_FLAGS.SWP_NOSIZE |
            SET_WINDOW_POS_FLAGS.SWP_NOZORDER);
    }

    public static uint GetDpiForWindow(IntPtr hwnd) => PInvoke.GetDpiForWindow(new HWND(hwnd));

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
}