using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Shell;

namespace Hyperbar.Windows.Interop;
public class WindowMessageListener :
    IDisposable
{
    private readonly nint hwnd;
    private SUBCLASSPROC? callback;

    protected WindowMessageListener(IntPtr hwnd)
    {
        this.hwnd = hwnd;
        Set();
    }

    ~WindowMessageListener()
    {
        Dispose();
    }

    public static WindowMessageListener Create(IntPtr hwnd) => new(hwnd);

    public void Dispose() => Remove();

    private void Remove()
    {
        PInvoke.RemoveWindowSubclass(new HWND(hwnd), callback, 101);
        callback = null;
    }

    private void Set()
    {
        callback = new SUBCLASSPROC(WindowProc);
        PInvoke.SetWindowSubclass(new HWND(hwnd), callback, 101, 0);
    }
    private LRESULT WindowProc(HWND hWnd, uint uMsg, 
        WPARAM wParam,
        LPARAM lParam, 
        nuint uIdSubclass,
        nuint dwRefData)
    {
        return PInvoke.DefSubclassProc(hWnd, uMsg, wParam, lParam);
    }
}
