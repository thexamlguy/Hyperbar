using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.UI.KeyboardAndMouseInput;

namespace Hyperbar.Interop.Windows;


public class VirtualKeyboard : 
    IVirtualKeyboard
{
    private readonly int[] extendedKeys = [
        165,    // RightMenu
        164,    // LeftMenu
        144,    // NumberKeyLock
        45,     // Insert
        46,     // Delete
        36,     // Home
        35,     // End
        36,     // Up,
        40,     // Down,
        37,     // Left
        39,     // Right,
        93,     // Application,
        92,     // RightWindows
        91      // LeftWindows
       ];

    public void Send(int key, params int[] modifierKeys)
    {
        foreach (int modiferKey in modifierKeys)
        {
            Press(modiferKey);
        }

        Press(key);
        Release(key);

        foreach (int modifierKey in modifierKeys.Reverse())
        {
            Release(modifierKey);
        }
    }

    private void Press(int key) => Send(key, true);

    private void Release(int key) => Send(key, false);

    private unsafe void Send(int key,
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

        if (extendedKeys.Contains((int)key))
        {
            flags |= KEYBD_EVENT_FLAGS.KEYEVENTF_EXTENDEDKEY;
        }

        input.Anonymous.ki.dwFlags = flags;
        input.Anonymous.ki.time = 0;
        input.Anonymous.ki.dwExtraInfo = new nuint();

        PInvoke.SendInput(new Span<INPUT>(ref input), Marshal.SizeOf(input));
    }
}