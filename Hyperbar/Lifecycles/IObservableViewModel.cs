using System.Windows.Input;

namespace Hyperbar;

public interface IObservableViewModel
{
    ICommand InitializeCommand { get; }
}