namespace Hyperbar.Windows.Win32
{
    public interface IVirtualKeyboard
    {
        void Send(int key, params int[] modifierKeys);
    }
}