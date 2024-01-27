namespace Hyperbar.Interop.Windows;

public interface IVirtualKeyboard
{
    void Send(int key, params int[] modifierKeys);
}