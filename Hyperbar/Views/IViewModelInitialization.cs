using System.Windows.Input;

namespace Hyperbar;

public interface IViewModelInitialization
{
    ICommand Initialize { get; }

    Task InitializeAsync();
}
