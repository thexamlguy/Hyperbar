using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;
using Windows.Win32.Graphics.Gdi;

namespace Hyperbar.Windows.Interop;

public class Screen
{
    private const int CCHDEVICENAME = 32;
    private const int PRIMARY_MONITOR = unchecked((int)0xBAADF00D);
    private static readonly bool multiMonitorSupport;

    private readonly IntPtr monitorHandle;

    static Screen()
    {
        multiMonitorSupport = PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_CMONITORS) != 0;
    }

    private Screen(IntPtr monitorHandle)
    {
        if (!multiMonitorSupport || monitorHandle == PRIMARY_MONITOR)
        {
            Bounds = SystemInformationHelper.VirtualScreen;
            Primary = true;
            DeviceName = "DISPLAY";
        }
        else
        {
            MonitorData monitorData = GetMonitorData(monitorHandle);

            Bounds = new Rect(monitorData.MonitorRect.left, monitorData.MonitorRect.top, monitorData.MonitorRect.right - monitorData.MonitorRect.left, monitorData.MonitorRect.bottom - monitorData.MonitorRect.top);
            Primary = (monitorData.Flags & (int)MonitorFlag.MONITOR_DEFAULTTOPRIMARY) != 0;
            DeviceName = monitorData.DeviceName;
        }

        this.monitorHandle = monitorHandle;
    }

    private enum MonitorFlag : uint
    {
        MONITOR_DEFAULTTONULL = 0,
        MONITOR_DEFAULTTOPRIMARY = 1,
        MONITOR_DEFAULTTONEAREST = 2
    }

    public Rect Bounds { get; }

    public string DeviceName { get; }

    public bool Primary { get; }

    public Rect WorkingArea => GetWorkingArea();

    public static Screen FromHandle(IntPtr handle)
    {
        return multiMonitorSupport ? new Screen(PInvoke.MonitorFromWindow((HWND)handle, MONITOR_FROM_FLAGS.MONITOR_DEFAULTTONEAREST)) : new Screen((IntPtr)PRIMARY_MONITOR);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Screen monitor) return false;
        return monitorHandle == monitor.monitorHandle;
    }

    public override int GetHashCode()
    {
        return (int)monitorHandle;
    }

    [DllImport("user32.dll", EntryPoint = "GetMonitorInfo", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool GetMonitorInfoEx(IntPtr hMonitor, ref MonitorData lpmi);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool SystemParametersInfo(int nAction, int nParam, ref RECT rc, int nUpdate);

    private MonitorData GetMonitorData(IntPtr monitorHandle)
    {
        MonitorData monitorData = new();
        monitorData.Size = Marshal.SizeOf(monitorData);
        GetMonitorInfoEx(monitorHandle, ref monitorData);

        return monitorData;
    }

    private Rect GetWorkingArea()
    {
        if (!multiMonitorSupport || monitorHandle == PRIMARY_MONITOR)
        {
            return SystemInformationHelper.WorkingArea;
        }

        MonitorData monitorData = GetMonitorData(monitorHandle);
        return new Rect(monitorData.WorkAreaRect.left, monitorData.WorkAreaRect.top, monitorData.WorkAreaRect.right - monitorData.WorkAreaRect.left, monitorData.WorkAreaRect.bottom - monitorData.WorkAreaRect.top);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct MonitorData
    {
        public int Size;
        public RECT MonitorRect;
        public RECT WorkAreaRect;
        public uint Flags;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
        public string DeviceName;
    }
}
