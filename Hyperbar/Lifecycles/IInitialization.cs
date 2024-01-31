using System.Windows.Input;

namespace Hyperbar;

public interface IInitialization2
{
    ICommand Initialize { get; }

    Task InitializeAsync();
}
