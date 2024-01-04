using System;
using System.Linq;
using System.Runtime.InteropServices;
using Windows.System;
using Windows.Win32;
using Windows.Win32.UI.KeyboardAndMouseInput;

namespace Hyperbar.Desktop.Win32;

public class KeyIntrop
{
    private static readonly VirtualKey[] ExtendedKeys = [
        VirtualKey.Menu,
        VirtualKey.Menu,
        VirtualKey.NumberKeyLock,
        VirtualKey.Insert,
        VirtualKey.Delete,
        VirtualKey.Home,
        VirtualKey.End,
        VirtualKey.Up,
        VirtualKey.Down,
        VirtualKey.Left,
        VirtualKey.Right,
        VirtualKey.Application,
        VirtualKey.RightWindows,
        VirtualKey.LeftWindows ];

    public static void Press(VirtualKey key) => SendKey(key, true);

    public static void Release(VirtualKey key) => SendKey(key, false);


    public static unsafe void Type(VirtualKey key, 
        params VirtualKey[] modifierKeys)
    {
        foreach (VirtualKey modiferKey in modifierKeys)
        {
            Press(modiferKey);
        }

        Press(key);
        Release(key);

        foreach (VirtualKey modifierKey in modifierKeys.Reverse())
        {
            Release(modifierKey);
        }
    }

    private static unsafe void SendKey(VirtualKey key, 
        bool pressed)
    {
        INPUT input = new()
        {
            type = INPUT_TYPE.INPUT_KEYBOARD
        };

        input.Anonymous.ki.wVk = (ushort)key;
        input.Anonymous.ki.wScan = (ushort)PInvoke.MapVirtualKey(input.Anonymous.ki.wVk, 0);

        KEYBD_EVENT_FLAGS flags = 0;

        if (input.Anonymous.ki.wScan > 0)
        {
            flags |= KEYBD_EVENT_FLAGS.KEYEVENTF_SCANCODE;
        }

        if (!pressed)
        {
            flags |= KEYBD_EVENT_FLAGS.KEYEVENTF_KEYUP;
        }

        if (ExtendedKeys.Contains(key))
        {
            flags |= KEYBD_EVENT_FLAGS.KEYEVENTF_EXTENDEDKEY;
        }

        input.Anonymous.ki.dwFlags = flags;
        input.Anonymous.ki.time = 0;
        input.Anonymous.ki.dwExtraInfo = new nuint();

        PInvoke.SendInput(new Span<INPUT>(ref input), Marshal.SizeOf(input));
    }
}
