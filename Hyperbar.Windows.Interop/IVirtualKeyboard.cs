namespace Hyperbar.Windows.Interop
{
    public interface IVirtualKeyboard
    {
        void Send(int key, params int[] modifierKeys);
    }
}